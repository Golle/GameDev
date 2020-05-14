using System;

namespace Titan.Graphics.Buffers
{
    public interface IVertexBuffer : IDisposable
    {
        void Bind();
    }
    public interface IVertexBuffer<T> : IVertexBuffer
    {

    }
}
