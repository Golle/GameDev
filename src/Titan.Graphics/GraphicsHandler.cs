using System;
using System.Numerics;
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
                    Debug = false,
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

        [StructLayout(LayoutKind.Sequential)]
        public struct Vertex
        {
            public float X;
            public float Y;
            public float Z;
            public float R;
            public float G;
            public float B;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ConstantBuffer
        {
            public Matrix4x4 Transformation;
        }

        /*
         *CreateOrthographic
            CreateOrthographicOffCenter
            CreatePerspective
            CreatePerspectiveOffCenter
            CreatePerspectiveFieldOfView
            CreateLookAt
         *
         */
        public static Matrix4x4 CreatePerspectiveLH1(float width, float height, float nearPlaneDistance, float farPlaneDistance)
        {
            Matrix4x4 matrix = default;
            matrix.M11 = 2 * nearPlaneDistance / width;
            matrix.M22 = 2 * nearPlaneDistance / height;
            matrix.M33 = farPlaneDistance / (farPlaneDistance - nearPlaneDistance);
            matrix.M34 = 1f;
            matrix.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            return matrix;

        }

        public unsafe struct TestStru
        {
            public fixed float Colors[4];
        }


        private float _angle;
        public unsafe void DrawTestTriangle()
        {

            var vertices = new[]
            {
                new Vertex {X = -1f, Y = -1f, Z = -1f, R = 1f},
                new Vertex {X = 1f, Y = -1f, Z = -1f, G = 1f},
                new Vertex {X = -1f, Y = 1f, Z = -1f, B = 1f},
                new Vertex {X = 1f, Y = 1f, Z = -1f, R = 1f},
                new Vertex {X = -1f, Y = -1f, Z = 1f, G = 1f},
                new Vertex {X = 1f, Y = -1f, Z = 1f, B = 1f},
                new Vertex {X = -1f, Y = 1f, Z = 1f, R = 1f},
                new Vertex {X = 1f, Y = 1f, Z = 1f, G = 1f},
            };
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
            desc.ByteWidth = (uint) (vertices.Length * (3 * sizeof(float) + 3 * sizeof(float)));
            desc.StructureByteStride = 3 * sizeof(float) + 3 * sizeof(float);
            using var buffer = _device.CreateBuffer(desc, resourceData);
            _device.Context.SetVertexBuffer(0, buffer, desc.StructureByteStride, 0u);

            // index buffer
            var indices = new short[]
            {
                0,2,1,  2,3,1,
                1,3,5,  3,7,5,
                2,6,3,  3,6,7,
                4,5,7,  4,7,6,
                0,4,2,  2,4,6,
                0,1,4,  1,5,4
            };
            D3D11SubresourceData indicesResourceData = default;
            fixed (void* p = indices)
            {
                indicesResourceData.pSysMem = p;
            }
            D3D11BufferDesc indexDesc = default;
            indexDesc.BindFlags = D3D11BindFlag.IndexBuffer;
            indexDesc.Usage = D3D11Usage.Default;
            indexDesc.CpuAccessFlags = D3D11CpuAccessFlag.Unspecified;
            indexDesc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            indexDesc.ByteWidth = (uint) (indices.Length * sizeof(short));
            indexDesc.StructureByteStride = sizeof(short);
            using var indexBuffer = _device.CreateBuffer(indexDesc, indicesResourceData);
            _device.Context.SetIndexBuffer(indexBuffer, DxgiFormat.R16Uint, 0u);


            //if (_inputManager.Keyboard.IsKeyDown(KeyCode.Up))
            {
                _angle += 0.02f;
            }

            if (_inputManager.Keyboard.IsKeyDown(KeyCode.Down))
            {
                _angle -= 0.01f;
            }


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
                    Format = DxgiFormat.R32G32B32Float,
                    InputSlot = 0,
                    AlignedByteOffset = 0,
                    InstanceDataStepRate = 0,
                    InputSlotClass = D3D11InputClassification.PerVertexData
                },
                new D3D11InputElementDesc
                {
                    SemanticName = "COLOR",
                    SemanticIndex = 0,
                    Format = DxgiFormat.R32G32B32Float,
                    InputSlot = 0,
                    AlignedByteOffset = 12u,
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


            _window.Update();
            var mousePosition = _inputManager.Mouse.Position;
            ConstantBuffer cb = default;

            var x = mousePosition.X / (_window.Width / 2f) - 1;
            var y = -mousePosition.Y / (_window.Height / 2f) + 1;
            var perspectiveLh = CreatePerspectiveLH1(1f, 3f/4f, 0.5f, 10f);
            cb.Transformation =
                Matrix4x4.Transpose(
                    Matrix4x4.CreateRotationZ(_angle) *
                    Matrix4x4.CreateRotationX(_angle) *
                    Matrix4x4.CreateTranslation(x, y, 4f) *
                    perspectiveLh
                );


            D3D11BufferDesc constantBufferDesc;
            constantBufferDesc.BindFlags = D3D11BindFlag.ConstantBuffer;
            constantBufferDesc.Usage = D3D11Usage.Dynamic;
            constantBufferDesc.CpuAccessFlags = D3D11CpuAccessFlag.Write;
            constantBufferDesc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            constantBufferDesc.ByteWidth = (uint)sizeof(Matrix4x4);
            constantBufferDesc.StructureByteStride = 0;

            D3D11SubresourceData constantBufferResource = default;
            constantBufferResource.pSysMem = &cb;
            using var constantBuffer = _device.CreateBuffer(constantBufferDesc, constantBufferResource);

            _device.Context.SetConstantBuffer(0, constantBuffer);



            //_device.Context.Draw((uint) vertices.Length, 0);
            _device.Context.DrawIndexed((uint) indices.Length, 0, 0);

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
