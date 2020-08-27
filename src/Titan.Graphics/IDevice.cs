using System;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics
{
    public interface IDevice : IDisposable
    {
        IDeviceContext ImmediateContext { get; }
        IDeviceContext CreateDeferredContext();
        IIndexBuffer CreateIndexBuffer(in short[] indices);
        IIndexBuffer CreateIndexBuffer(uint size);
        IVertexBuffer CreateVertexBuffer<T>(uint numberOfVertices) where T : unmanaged;
        IVertexBuffer CreateVertexBuffer<T>(in T[] initialData) where T : unmanaged;
        IConstantBuffer<T> CreateConstantBuffer<T>() where T : unmanaged;
        IConstantBuffer<T> CreateConstantBuffer<T>(in T initialData) where T : unmanaged;
        IVertexShader CreateVertexShader(IBlob vertexShaderBlob);
        IPixelShader CreatePixelShader(IBlob pixelShaderBlob);
        IInputLayout CreateInputLayout(VertexLayout vertexLayout, IBlob vertexShaderBlob);
        ITexture2D CreateTexture2D(uint width, uint height, in byte[] pixels);
        ISampler CreateSampler(bool point = false);
        IBlendState CreateBlendState();
        void SetPrimitiveTopology(PrimitiveTopology topology);
        void BeginRender();
        void EndRender();
        void DrawIndexed(uint numberOfIndices, uint startIndexLocation, int baseVertexLocation);
        void Draw(uint vertexCount, uint startLocation);
    }
}
