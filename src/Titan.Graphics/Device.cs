using System;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Titan.D3D11;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;
using Titan.Graphics.Buffers;
using Titan.Graphics.Shaders;

namespace Titan.Graphics
{
    internal class Device : IDevice
    {
        private readonly ID3D11Device _device;
        private readonly ID3D11RenderTargetView _renderTarget;
        private readonly ID3D11DepthStencilView _depthStencilView;
        private readonly ID3DCommon _common;

        private readonly Color _clearColor = new Color{Blue = 0.1f, Alpha = 1f};

        private readonly bool _vSync = true;

        public Device(ID3D11Device device, ID3D11RenderTargetView renderTarget, ID3D11DepthStencilView depthStencilView, ID3DCommon common)
        {
            _device = device ?? throw new ArgumentNullException(nameof(device));
            _renderTarget = renderTarget ?? throw new ArgumentNullException(nameof(renderTarget));
            _depthStencilView = depthStencilView ?? throw new ArgumentNullException(nameof(depthStencilView));
            _common = common ?? throw new ArgumentNullException(nameof(common));
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
                    return new IndexBuffer(_device.CreateBuffer(desc, data), indices);
                }
            }
        }

        public IIndexBuffer CreateIndexBuffer(uint size)
        {
            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.IndexBuffer;
            desc.Usage = D3D11Usage.Dynamic;
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Write;
            desc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            desc.ByteWidth = size * sizeof(short);
            desc.StructureByteStride = sizeof(short);

            return new IndexBuffer(_device.CreateBuffer(desc), size);
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
            desc.Usage = D3D11Usage.Dynamic;
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Write;
            desc.MiscFlags = D3D11ResourceMiscFlag.Unspecified;
            desc.ByteWidth = (numberOfVertices * size);
            desc.StructureByteStride = size;

            return new VertexBuffer<T>(_device.CreateBuffer(desc));
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
                    return new VertexBuffer<T>(_device.CreateBuffer(desc, data));
                }
            }
        }

        public IConstantBuffer<T> CreateConstantBuffer<T>(in T initialData) where T : unmanaged
        {
            var size = (uint)Marshal.SizeOf<T>();
            CheckAlignment(size);

            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.ConstantBuffer;
            desc.Usage = D3D11Usage.Dynamic; // not sure about this one
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Write; // not sure about this one
            desc.ByteWidth = size;

            unsafe
            {
                fixed (void* initialDataPointer = &initialData)
                {
                    D3D11SubresourceData data = default;
                    data.pSysMem = initialDataPointer;
                    return new ConstantBuffer<T>(_device.CreateBuffer(desc, data));
                }
            }
        }

        public IVertexShader CreateVertexShader(string filename)
        {
            // not sure if we should read the file here, maybe look at other ways to do it
            // maybe we should load the blob in some other place and pass the blob here (buffer pointer + size)
            using var blob = _common.ReadFileToBlob(filename);
            return new VertexShader(_device.Context, _device.CreateVertexShader(blob));
        }

        public IPixelShader CreatePixelShader(string filename)
        {
            // not sure if we should read the file here, maybe look at other ways to do it
            // maybe we should load the blob in some other place and pass the blob here (buffer pointer + size)
            using var blob = _common.ReadFileToBlob(filename);
            return new PixelShader(_device.Context, _device.CreatePixelShader(blob));
        }

        public IConstantBuffer<T> CreateConstantBuffer<T>() where T : unmanaged
        {
            var size = (uint)Marshal.SizeOf<T>();
            CheckAlignment(size);

            D3D11BufferDesc desc = default;
            desc.BindFlags = D3D11BindFlag.ConstantBuffer;
            desc.Usage = D3D11Usage.Dynamic; // not sure about this one
            desc.CpuAccessFlags = D3D11CpuAccessFlag.Write; // not sure about this one
            desc.ByteWidth = size;
            return new ConstantBuffer<T>(_device.CreateBuffer(desc));
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
