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

        private readonly Color _clearColor = new Color{B = 0.6f, A = 1f};

        private readonly bool _vSync = false;

        public IDeviceContext ImmediateContext { get; }
        public Device(ID3D11Device device, ID3D11RenderTargetView renderTarget, ID3D11DepthStencilView depthStencilView)
        {
            _device = device ?? throw new ArgumentNullException(nameof(device));
            _renderTarget = renderTarget ?? throw new ArgumentNullException(nameof(renderTarget));
            _depthStencilView = depthStencilView ?? throw new ArgumentNullException(nameof(depthStencilView));
            ImmediateContext = new DeviceContext(device.Context);
        }

        public IDeviceContext CreateDeferredContext()
        {
            throw new NotImplementedException();
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

        public IVertexBuffer CreateVertexBuffer<T>(uint numberOfVertices) where T : unmanaged
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

            return new VertexBuffer(_device.CreateBuffer(desc), size, 0);
        }

        public IVertexBuffer CreateVertexBuffer<T>(in T[] initialData) where T : unmanaged
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
                    return new VertexBuffer(_device.CreateBuffer(desc, data), size, (uint) initialData.Length);
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
            desc.Format = DxgiFormat.R8G8B8A8Unorm;
            desc.SampleDesc.Count = 1;
            desc.SampleDesc.Quality = 0;
            desc.CPUAccessFlags = D3D11CpuAccessFlag.Unspecified;
            desc.MiscFlags = 0;
            
            ID3D11Texture2D texture;
            unsafe
            {
                fixed (byte* b = pixels)
                {
                    D3D11SubresourceData data = default;
                    data.SysMemPitch = width * 4; // Width * size of R8G8B8A8Uint
                    data.pSysMem = b;
                    texture = _device.CreateTexture2D(desc, data);
                    
                }
            }

            D3D11ShaderResourceViewDesc resourceViewDesc = default;
            resourceViewDesc.Format = desc.Format;
            resourceViewDesc.ViewDimension = D3D11SrvDimension.Texture2D;
            resourceViewDesc.Texture2D.MostDetailedMip = 0;
            resourceViewDesc.Texture2D.MipLevels = 1;

            var textureView = _device.CreateShaderResourceView(texture, resourceViewDesc);

            return new Texture2D(_device.Context, texture, textureView, width, height);
        }

        public ISampler CreateSampler(bool point = false)
        {
            D3D11SamplerDesc desc = default;
            desc.Filter = point ? D3D11Filter.MaximumMinMagMipPoint : D3D11Filter.MinMagMipLinear;
            desc.AddressU = D3D11TextureAddressMode.Wrap;
            desc.AddressV = D3D11TextureAddressMode.Wrap;
            desc.AddressW = D3D11TextureAddressMode.Wrap;

            var sampler = _device.CreateSamplerState(desc);

            return new Sampler(_device.Context, sampler);

        }

        public IBlendState CreateBlendState()
        {
            var desc = D3D11BlendDesc.Default;
            desc.RenderTargets[0].BlendEnable = true;
            desc.RenderTargets[0].RenderTargetWriteMask = D3D11ColorWriteEnable.All;
            desc.RenderTargets[0].SrcBlend = D3D11Blend.SrcAlpha;
            desc.RenderTargets[0].DestBlend = D3D11Blend.InvSrcAlpha;
            
            var blendState = _device.CreateBlendState(desc);

            return new BlendState(_device.Context, blendState);
        }

        public void SetPrimitiveTopology(PrimitiveTopology topology)
        {
            _device.Context.SetPrimitiveTopology((D3D11PrimitiveTopology)topology);
        }

        public void BeginRender()
        {
            _device.Context.ClearRenderTargetView(_renderTarget, _clearColor);
            _device.Context.ClearDepthStencilView(_depthStencilView, D3D11ClearFlag.Depth, 1f, 0);
        }

        public void EndRender()
        {
            _device.SwapChain.Present(_vSync);
        }

        public void DrawIndexed(uint numberOfIndices, uint startIndexLocation, int baseVertexLocation)
        {
            _device.Context.DrawIndexed(numberOfIndices, startIndexLocation, baseVertexLocation);
        }

        public void Draw(uint vertexCount, uint startLocation)
        {
            _device.Context.Draw(vertexCount, startLocation);
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
