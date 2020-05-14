using System;
using Titan.D3D11.Bindings.Models;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;

namespace Titan.Graphics
{
    public interface IDevice : IDisposable
    {
        IIndexBuffer CreateIndexBuffer(in short[] indices);
        IIndexBuffer CreateIndexBuffer(uint size);
        IVertexBuffer<T> CreateVertexBuffer<T>(uint numberOfVertices) where T : unmanaged;
        IVertexBuffer<T> CreateVertexBuffer<T>(in T[] initialData) where T : unmanaged;
        IConstantBuffer<T> CreateConstantBuffer<T>() where T : unmanaged;
        IConstantBuffer<T> CreateConstantBuffer<T>(in T initialData) where T : unmanaged;

        IVertexShader CreateVertexShader(IBlob vertexShaderBlob);
        IPixelShader CreatePixelShader(IBlob pixelShaderBlob);


        IInputLayout CreateInputLayout(VertexLayout vertexLayout, IBlob vertexShaderBlob);

        void BeginRender();
        void EndRender();
        void DrawIndexed(uint numberOfIndices, uint startIndexLocation, int baseVertexLocation);
    }
}
