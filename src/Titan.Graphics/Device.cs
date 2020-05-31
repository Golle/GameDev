using System;
using System.Runtime.InteropServices;
using Titan.D3D11;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics
{
    internal class Device : IDevice
    {
        private readonly ID3D11Device _device;
        private readonly ID3D11RenderTargetView _renderTarget;
        private readonly ID3D11DepthStencilView _depthStencilView;

        private readonly Color _clearColor = new Color{Blue = 0.1f, Alpha = 1f};

        private readonly bool _vSync = true;

        public Device(ID3D11Device device, ID3D11RenderTargetView renderTarget, ID3D11DepthStencilView depthStencilView)
        {
            _device = device ?? throw new ArgumentNullException(nameof(device));
            _renderTarget = renderTarget ?? throw new ArgumentNullException(nameof(renderTarget));
            _depthStencilView = depthStencilView ?? throw new ArgumentNullException(nameof(depthStencilView));
        }

        public IIndexBuffer CreateIndexBuffer(in short[] indices)
        {
            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.IndexBuffer;
            desc.Usage = D3D11Usage.Default;
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Unspecified;
            desc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            desc.ByteWidth = (uint) (indices.Length * sizeof(short));
            desc.StructureByteStride = sizeof(short);
            unsafe
            {
                fixed (void* indicesPointer = indices)
                {
                    D3D11SubresourceData data = default;
                    data.pSysMem = indicesPointer;
                    return new IndexBuffer(_device.Context, _device.CreateBuffer(desc, data), indices);
                }
            }
        }

        public IIndexBuffer CreateIndexBuffer(uint size)
        {
            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.IndexBuffer;
            desc.Usage = D3D11Usage.Default;
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Unspecified;
            desc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            desc.ByteWidth = size * sizeof(short);
            desc.StructureByteStride = sizeof(short);

            return new IndexBuffer(_device.Context, _device.CreateBuffer(desc), size); 
        }

        public IVertexBuffer<T> CreateVertexBuffer<T>(uint numberOfVertices) where T : unmanaged
        {
            if (numberOfVertices == 0)
            {
                throw new InvalidOperationException($"{nameof(numberOfVertices)} must be greater than 0.");
            }
            var size = (uint)Marshal.SizeOf<T>();
            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.VertexBuffer;
            desc.Usage = D3D11Usage.Default;
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Unspecified;
            desc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            desc.ByteWidth = (numberOfVertices * size);
            desc.StructureByteStride = size;

            return new VertexBuffer<T>(_device.Context, _device.CreateBuffer(desc), size);
        }

        public IVertexBuffer<T> CreateVertexBuffer<T>(in T[] initialData) where T : unmanaged
        {
            if (initialData == null)
            {
                throw new ArgumentNullException(nameof(initialData));
            }
            var size = (uint)Marshal.SizeOf<T>();
            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.VertexBuffer;
            desc.Usage = D3D11Usage.Default;
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Unspecified;
            desc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            desc.ByteWidth = (uint) (initialData.Length * size);
            desc.StructureByteStride = size;
            unsafe
            {
                fixed (void* initialDataPointer = initialData)
                {
                    D3D11SubresourceData data = default;
                    data.pSysMem = initialDataPointer;
                    return new VertexBuffer<T>(_device.Context, _device.CreateBuffer(desc, data), size);
                }
            }
        }

        public IConstantBuffer<T> CreateConstantBuffer<T>() where T : unmanaged
        {
            var size = (uint)Marshal.SizeOf<T>();
            CheckAlignment(size);

            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.ConstantBuffer;
            desc.Usage = D3D11Usage.Default; // not sure about this one
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Unspecified; // not sure about this one
            desc.ByteWidth = size;
            return new ConstantBuffer<T>(_device.Context, _device.CreateBuffer(desc));
        }

        public IConstantBuffer<T> CreateConstantBuffer<T>(in T initialData) where T : unmanaged
        {
            var size = (uint)Marshal.SizeOf<T>();
            CheckAlignment(size);

            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.ConstantBuffer;
            desc.Usage = D3D11Usage.Default; // not sure about this one
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Unspecified; // not sure about this one
            desc.ByteWidth = size;

            unsafe
            {
                fixed (void* initialDataPointer = &initialData)
                {
                    D3D11SubresourceData data = default;
                    data.pSysMem = initialDataPointer;
                    return new ConstantBuffer<T>(_device.Context, _device.CreateBuffer(desc, data));
                }
            }
        }

        public IVertexShader CreateVertexShader(IBlob vertexShaderBlob)
        {
            var shader = _device.CreateVertexShader(vertexShaderBlob.Buffer, vertexShaderBlob.Size);

            return new VertexShader(_device.Context, shader);
        }

        public IPixelShader CreatePixelShader(IBlob pixelShaderBlob)
        {
            var shader = _device.CreatePixelShader(pixelShaderBlob.Buffer, pixelShaderBlob.Size);

            return new PixelShader(_device.Context, shader);
        }

        public IInputLayout CreateInputLayout(VertexLayout vertexLayout, IBlob vertexShaderBlob)
        {
            var layout = _device.CreateInputLayout(vertexLayout.Descriptors, vertexShaderBlob.Buffer, vertexShaderBlob.Size);
            return new InputLayout(_device.Context, layout);
        }

        public ITexture2D CreateTexture2D(uint width, uint height, in byte[] pixels)
        {
            D3D11Texture2DDesc desc = default;
            desc.Height = height;
            desc.Width = width;
            desc.BindFlags = D3D11BindFlag.ShaderResource;
            desc.MipLevels = 1; // TODO: add support for this
            desc.ArraySize = 1; // TODO: add support for this
            desc.Format = DxgiFormat.R8G8B8A8Uint;
            desc.SampleDesc.Count = 1;
            desc.SampleDesc.Quality = 0;
            desc.CPUAccessFlags = D3D11CpuAccessFlag.Unspecified;
            desc.MiscFlags = 0;

            D3D11SubresourceData data = default;
            data.SysMemPitch = width * 4; // Width * size of R8G8B8A8Uint
            unsafe
            {
                fixed (byte* b = pixels)
                {
                    data.pSysMem = b;
                    return new Texture2D(_device.CreateTexture2D(desc, data));
                }
            }
        }

        public void BeginRender()
        {
            _device.Context.ClearRenderTargetView(_renderTarget, _clearColor);
            _device.Context.ClearDepthStencilView(_depthStencilView, D3D11ClearFlag.Depth, 1f, 0);
            
            // TODO: this is tempory
            _device.Context.SetPrimitiveTopology(D3D11PrimitiveTopology.D3D11PrimitiveTopologyTrianglelist);
        }

        public void EndRender()
        {
            _device.SwapChain.Present(_vSync);
        }

        public void DrawIndexed(uint numberOfIndices, uint startIndexLocation, int baseVertexLocation)
        {
            _device.Context.DrawIndexed(numberOfIndices, startIndexLocation, baseVertexLocation);
        }

        private static void CheckAlignment(uint bytes)
        {
            if (bytes % 16 != 0)
            {
                throw new InvalidOperationException($"The size of the type is {bytes} bytes and is not 16 bytes aligned.");
            }
        }

        public void Dispose()
        {
            _depthStencilView.Dispose();
            _renderTarget.Dispose();
            _device.Dispose();
        }
    }
}
