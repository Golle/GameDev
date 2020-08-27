using System;

namespace Titan.Graphics.Buffers
{
    public interface IVertexBuffer : IResource, IDisposable
    {
        uint NumberOfVertices { get; }
        uint Strides { get; }
        uint Offset { get; }
    }
}
