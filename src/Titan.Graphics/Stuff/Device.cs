using System;
using Titan.D3D11;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;
using Titan.Graphics.Buffers;

namespace Titan.Graphics.Stuff
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

        public IIndexBuffer CreateIndexBuffer()
        {
            throw new NotImplementedException();
        }

        public IVertexBuffer<T> CreateVertexBuffer<T>(uint numberOfVertices) where T : unmanaged, IVertex
        {
            if (numberOfVertices == 0)
            {
                throw new InvalidOperationException($"{nameof(numberOfVertices)} must be greater than 0.");
            }
            var size = default(T).Size();
            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.VertexBuffer;
            desc.Usage = D3D11Usage.Dynamic;
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Write;
            desc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            desc.ByteWidth = (numberOfVertices * size);
            desc.StructureByteStride = size;

            return new VertexBuffer<T>(_device.CreateBuffer(desc));
        }

        public IVertexBuffer<T> CreateVertexBuffer<T>(in T[] initialData) where T : unmanaged, IVertex
        {
            if (initialData == null)
            {
                throw new ArgumentNullException(nameof(initialData));
            }
            var size = default(T).Size();
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
                    return new VertexBuffer<T>(_device.CreateBuffer(desc, data));
                }
            }

        }

        public IConstantBuffer CreateConstantBuffer()
        {
            throw new NotImplementedException();
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

        public void Dispose()
        {
            _device.Dispose();
        }
    }
}
