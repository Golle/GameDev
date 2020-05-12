using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Titan.Core.EventSystem;
using Titan.D3D11;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;
using Titan.Windows.Input;
using Titan.Windows.Window;
using Color = Titan.D3D11.Color;

namespace Titan.Graphics.Stuff
{
    public class GraphicsHandler : IGraphicsHandler
    {
        private readonly IWindowCreator _windowCreator;
        private readonly ID3D11DeviceFactory _d3D11DeviceFactory;
        private readonly IInputManager _inputManager;
        private readonly ID3DCommon _d3DCommon;

        private IWindow _window;
        private ID3D11Device _device;
        private ID3D11RenderTargetView _renderTarget;
        private ID3D11DepthStencilView _depthStencilView;

        public GraphicsHandler(IWindowCreator windowCreator, ID3D11DeviceFactory d3D11DeviceFactory, IInputManager inputManager, ID3DCommon d3DCommon)
        {
            _windowCreator = windowCreator;
            _d3D11DeviceFactory = d3D11DeviceFactory;
            _inputManager = inputManager;
            _d3DCommon = d3DCommon;
        }

        public bool Initialize(string title, int width, int height)
        {
            _window = _windowCreator.CreateWindow(new CreateWindowArguments(title, width, height));

            _device = _d3D11DeviceFactory
                .Create(new CreateDeviceArguments(_window, 144, IntPtr.Zero, true));

            using var backBuffer = _device.SwapChain.GetBuffer(0, D3D11Resources.D3D11Resource);
            _renderTarget = _device.CreateRenderTargetView(backBuffer);

            D3D11DepthStencilDesc depthDesc = default;
            depthDesc.DepthEnable = true;
            depthDesc.DepthWriteMask = D3D11DepthWriteMask.All;
            depthDesc.DepthFunc = D3D11ComparisonFunc.Less;
            using var depthStencilState = _device.CreateDepthStencilState(depthDesc);
            _device.Context.OMSetDepthStencilState(depthStencilState, 1u);


            D3D11Texture2DDesc texture2DDesc = default;
            texture2DDesc.Height = (uint)_window.Height;
            texture2DDesc.Width = (uint) _window.Width;
            texture2DDesc.MipLevels = 1u;
            texture2DDesc.ArraySize = 1u;
            texture2DDesc.Format = DxgiFormat.D32Float;
            texture2DDesc.SampleDesc.Count = 1u;
            texture2DDesc.SampleDesc.Quality = 0;
            texture2DDesc.Usage = D3D11Usage.Default;
            texture2DDesc.BindFlags = D3D11BindFlag.DepthStencil;
            using var depthStencil = _device.CreateTexture2D(texture2DDesc);

            D3D11DepthStencilViewDesc viewDesc = default;
            viewDesc.Format = DxgiFormat.D32Float;
            viewDesc.ViewDimension = D3D11DsvDimension.Texture2D;
            viewDesc.Texture2D.MipSlice = 0u;
            _depthStencilView = _device.CreateDepthStencilView(depthStencil, viewDesc);

            _device.Context.OMSetRenderTargets(_renderTarget, _depthStencilView);
            _window.ShowWindow();

            return true;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Vertex
        {
            public float X;
            public float Y;
            public float Z;
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


        uint RGBAToUint(float r, float g, float b, float a = 1f)
        {
            var red = (int)(r * 255);
            var green = (int)(g * 255);
            var blue = (int)(b * 255);
            var alpha = (int)(a * 255);

            return (uint) (alpha | (blue << 8) | (green << 16) | (red << 24));
        }
        
        private float _angle;


        public unsafe void DrawTestTriangle(float x, float z, float angle)
        {
            
            var vertices = new[]
            {
                new Vertex {X = -1f, Y = -1f, Z = -1f},
                new Vertex {X = 1f, Y = -1f, Z = -1f},
                new Vertex {X = -1f, Y = 1f, Z = -1f},
                new Vertex {X = 1f, Y = 1f, Z = -1f},
                new Vertex {X = -1f, Y = -1f, Z = 1f},
                new Vertex {X = 1f, Y = -1f, Z = 1f},
                new Vertex {X = -1f, Y = 1f, Z = 1f},
                new Vertex {X = 1f, Y = 1f, Z = 1f},
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
            desc.ByteWidth = (uint) (vertices.Length * 3 * sizeof(float));
            desc.StructureByteStride = 3 * sizeof(float);
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
                //new D3D11InputElementDesc
                //{
                //    SemanticName = "COLOR",
                //    SemanticIndex = 0,
                //    Format = DxgiFormat.R32G32B32Float,
                //    InputSlot = 0,
                //    AlignedByteOffset = 12u,
                //    InstanceDataStepRate = 0,
                //    InputSlotClass = D3D11InputClassification.PerVertexData
                //}
            };

            using var inputLayout = _device.CreateInputLayout(elementDesc, vertexShaderBlob);
            _device.Context.SetInputLayout(inputLayout);

            _device.Context.SetPrimitiveTopology(D3D11PrimitiveTopology.D3D11PrimitiveTopologyTrianglelist);

            D3D11Viewport viewport = default;
            viewport.Width = _window.Width;
            viewport.Height = _window.Height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            _device.Context.SetViewport(viewport);


            ConstantBuffer cb = default;

            var perspectiveLh = CreatePerspectiveLH1(1f, 3f/4f, 0.5f, 10f);
            cb.Transformation =
                Matrix4x4.Transpose(
                    Matrix4x4.CreateRotationZ(angle) *
                    Matrix4x4.CreateRotationX(angle) *
                    Matrix4x4.CreateTranslation(x, 0f, z+4f) *
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

            _device.Context.VSSetConstantBuffer(0, constantBuffer);


            var colors = new[]
            {
                new Color(1, 0, 1),
                new Color(1, 0, 0),
                new Color(0, 1, 1),
                new Color(0, 0, 1),
                new Color(1, 1, 0),
                new Color(0, 1, 1),
            };

            D3D11BufferDesc constantBufferDesc2;
            constantBufferDesc2.BindFlags = D3D11BindFlag.ConstantBuffer;
            constantBufferDesc2.Usage = D3D11Usage.Dynamic;
            constantBufferDesc2.CpuAccessFlags = D3D11CpuAccessFlag.Write;
            constantBufferDesc2.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            constantBufferDesc2.ByteWidth = (uint)(sizeof(float)* 4 * colors.Length);
            constantBufferDesc2.StructureByteStride = 0;
            D3D11SubresourceData constantBufferResource2 = default;
            fixed (void* p = colors)
            {
                constantBufferResource2.pSysMem = p;
            }
            using var constantBuffer2 = _device.CreateBuffer(constantBufferDesc2, constantBufferResource2);
            _device.Context.PSSetConstantBuffer(0, constantBuffer2);



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
            _device.Context.ClearDepthStencilView(_depthStencilView, D3D11ClearFlag.Depth, 1f, 0);
            
            var x = mousePosition.X / (_window.Width / 2f) - 1;
            var y = -mousePosition.Y / (_window.Height / 2f) + 1;
            
            _angle += 0.02f;
            DrawTestTriangle(x, y, _angle);
            DrawTestTriangle(0, 0, _angle * 2f);

            _device.SwapChain.Present(true);

            return true;
        }

        public void Dispose()
        {
            _depthStencilView.Dispose();
            _renderTarget.Dispose();
            _device.Dispose();
        }
    }
}
