using System;
using Titan.Graphics.Buffers;

namespace Titan.Graphics.Stuff
{
    public interface IDevice : IDisposable
    {
        IIndexBuffer CreateIndexBuffer(in short[] indices);
        IIndexBuffer CreateIndexBuffer(uint size);
        IVertexBuffer<T> CreateVertexBuffer<T>(uint numberOfVertices) where T : unmanaged, IVertex;
        IVertexBuffer<T> CreateVertexBuffer<T>(in T[] initialData) where T : unmanaged, IVertex;
        IConstantBuffer<T> CreateConstantBuffer<T>() where T : unmanaged;
        IConstantBuffer<T> CreateConstantBuffer<T>(in T initialData) where T : unmanaged;

        void BeginRender();
        void EndRender();
    }
}
