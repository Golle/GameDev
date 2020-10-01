using System;
using Titan.D3D11.Compiler;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics
{
    public interface IDevice : IDisposable
    {
        ID3DCompiler TEMPORARYCompiler { get; }
        IDeviceContext ImmediateContext { get; }
        IRenderTarget BackBuffer { get; }
        IDepthStencil DepthStencil { get; }
        IDeferredDeviceContext CreateDeferredContext();
        IIndexBuffer CreateIndexBuffer(in int[] indices, BufferUsage usage = BufferUsage.Default, BufferAccessFlags flags = BufferAccessFlags.Default);
        IIndexBuffer CreateIndexBuffer(uint size, BufferUsage usage = BufferUsage.Default, BufferAccessFlags flags = BufferAccessFlags.Default);
        unsafe IIndexBuffer CreateIndexBuffer(void* data, int count, BufferUsage usage = BufferUsage.Default, BufferAccessFlags flags = BufferAccessFlags.Default);
        IVertexBuffer<T> CreateVertexBuffer<T>(uint numberOfVertices, BufferUsage usage = BufferUsage.Default, BufferAccessFlags flags = BufferAccessFlags.Default) where T : unmanaged;
        IVertexBuffer<T> CreateVertexBuffer<T>(in T[] initialData, BufferUsage usage = BufferUsage.Default, BufferAccessFlags flags = BufferAccessFlags.Default) where T : unmanaged;
        unsafe IVertexBuffer<T> CreateVertexBuffer<T>(void* data, int count, BufferUsage usage = BufferUsage.Default, BufferAccessFlags flags = BufferAccessFlags.Default) where T : unmanaged;
        IConstantBuffer<T> CreateConstantBuffer<T>(BufferUsage usage = BufferUsage.Default, BufferAccessFlags flags = BufferAccessFlags.Default) where T : unmanaged;
        IConstantBuffer<T> CreateConstantBuffer<T>(in T initialData, BufferUsage usage = BufferUsage.Default, BufferAccessFlags flags = BufferAccessFlags.Default) where T : unmanaged;
        IVertexShader CreateVertexShader(IBlob vertexShaderBlob);
        IPixelShader CreatePixelShader(IBlob pixelShaderBlob);
        IInputLayout CreateInputLayout(VertexLayout vertexLayout, IBlob vertexShaderBlob);
        ITexture2D CreateTexture2D(uint width, uint height, in byte[] pixels);
        ITexture2D CreateTexture2DRENDERTARGETPROTOTYPE(uint width, uint height);
        ISampler CreateSampler(bool point = false);
        IBlendState CreateBlendState();
        void EndRender();
        IRenderTarget CreateRenderTarget(IntPtr resource);
    }
}
