using System;

namespace Titan.Graphics.Buffers
{
    public interface IVertexBuffer : IDisposable {}
    public interface IVertexBuffer<T> : IVertexBuffer
    {

    }
}
