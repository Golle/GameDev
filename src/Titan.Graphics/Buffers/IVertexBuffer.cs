using System;
using Titan.Graphics.Renderer;

namespace Titan.Graphics.Buffers
{
    public interface IVertexBuffer : IDisposable
    {
        void Bind();
    }
    public interface IVertexBuffer<T> : IVertexBuffer
    {
        void SetData(in T[] vertices, int numberOfVertices);
    }
}
