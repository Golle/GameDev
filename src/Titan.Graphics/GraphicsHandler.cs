using System;
using System.Runtime.InteropServices;
using Titan.Core.EventSystem;
using Titan.D3D11;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan.Graphics
{
    public class GraphicsHandler : IGraphicsHandler
    {
        private readonly IWindowCreator _windowCreator;
        private readonly ID3D11DeviceFactory _d3D11DeviceFactory;
        private readonly IEventManager _eventManager;
        private readonly IInputManager _inputManager;
        private readonly ID3DCommon _d3DCommon;

        private IWindow _window;
        private ID3D11Device _device;
        private ID3D11RenderTargetView _renderTarget;

        public GraphicsHandler(IWindowCreator windowCreator, ID3D11DeviceFactory d3D11DeviceFactory, IEventManager eventManager, IInputManager inputManager, ID3DCommon d3DCommon)
        {
            _windowCreator = windowCreator;
            _d3D11DeviceFactory = d3D11DeviceFactory;
            _eventManager = eventManager;
            _inputManager = inputManager;
            _d3DCommon = d3DCommon;
        }

        public bool Initialize(string title, int width, int height)
        {
            _window = _windowCreator.CreateWindow(new CreateWindowArguments
            {
                Height = height, 
                Title = title, 
                Width = width
            });
            _device = _d3D11DeviceFactory
                .Create(new CreateDeviceArguments
                {
                    Adapter = IntPtr.Zero,
                    RefreshRate = 144, 
                    Debug = true,
                    Window = _window
                });

            using (var backBuffer = _device.SwapChain.GetBuffer(0, D3D11Resources.D3D11Resource))
            {
                _renderTarget = _device.CreateRenderTargetView(backBuffer);
            }

            _device.Context.SetRenderTargets(_renderTarget);
            _window.ShowWindow();

            return true;
        }

        [StructLayout(LayoutKind.Sequential, Size = 8)]
        public struct Vertex
        {
            public float X;
            public float Y;
        }


        public unsafe void DrawTestTriangle()
        {

            var vertices = new Vertex[3];

            vertices[0] = new Vertex { X = 0f, Y = 0.5f };
            vertices[1] = new Vertex { X = 0.5f, Y = -0.5f };
            vertices[2] = new Vertex { X = -0.5f, Y = -0.5f };

            D3D11SubresourceData resourceData = default;

            fixed (void* p = vertices)
            {
                resourceData.pSysMem = p;
            }

            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.VertexBuffer;
            desc.Usage = D3D11Usage.Default;
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Unspecified;
            desc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            desc.ByteWidth = 3 * 4 * 2;
            desc.StructureByteStride = 2 * 4;


            using var buffer = _device.CreateBuffer(desc, resourceData);
            _device.Context.SetVertexBuffer(0, buffer, desc.StructureByteStride, 0u);


            using var vertexShaderBlob = _d3DCommon.ReadFileToBlob("Shaders/VertexShader.cso");
            using var vertexShader = _device.CreateVertexShader(vertexShaderBlob);
            _device.Context.SetVertexShader(vertexShader);
            

            using var pixelShaderBlob = _d3DCommon.ReadFileToBlob("Shaders/PixelShader.cso");
            using var pixelShader = _device.CreatePixelShader(pixelShaderBlob);
            _device.Context.SetPixelShader(pixelShader);

            var elementDesc = new[]
            {
                new D3D11InputElementDesc
                {
                    SemanticName = "POSITION",
                    SemanticIndex = 0,
                    Format = DxgiFormat.R32G32Float,
                    InputSlot = 0,
                    AlignedByteOffset = 0,
                    InstanceDataStepRate = 0,
                    InputSlotClass = D3D11InputClassification.PerVertexData
                }
            };

            using var inputLayout = _device.CreateInputLayout(elementDesc, vertexShaderBlob);
            _device.Context.SetInputLayout(inputLayout);


            _device.Context.SetRenderTargets(_renderTarget);
            
            _device.Context.SetPrimitiveTopology(D3D11PrimitiveTopology.D3D11PrimitiveTopologyTrianglelist);

            D3D11Viewport viewport = default;
            viewport.Width = _window.Width;
            viewport.Height = _window.Height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            _device.Context.SetViewport(viewport);

            _device.Context.Draw((uint) vertices.Length, 0);

        }

        public bool Update()
        {
            if (_window.Update() == false)
            {
                return false;
            }
            
            var color = new Color { Alpha = 1f };
            if (_inputManager.Keyboard.IsKeyDown(KeyCode.A))
            {
                color.Red = 1f;
            }
            if (_inputManager.Keyboard.IsKeyDown(KeyCode.S))
            {
                color.Green = 1f;
            }
            if (_inputManager.Keyboard.IsKeyDown(KeyCode.D))
            {
                color.Blue = 1f;
            }
            while (_inputManager.Keyboard.TryGetChar(out var character))
            {
                //Console.Write(character);
            }

            var mouse = _inputManager.Mouse;
            var mousePosition = mouse.Position;
            
            _window.SetTitle($"x: {mousePosition.X} y: {mousePosition.Y}, left: {mouse.LeftButtonDown}, right: {mouse.RightButtonDown}");

            _device.Context.ClearRenderTargetView(_renderTarget, color);

            DrawTestTriangle();

            _device.SwapChain.Present(true);

            return true;
        }

        public void Dispose()
        {
            _renderTarget.Dispose();
            _device.Dispose();
        }
    }
}
