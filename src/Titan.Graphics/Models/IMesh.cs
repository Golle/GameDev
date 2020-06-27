using System;
using Titan.Graphics.Buffers;

namespace Titan.Graphics.Models
{
    public interface IMesh : IDisposable
    {
        public IVertexBuffer VertexBuffer { get; }
        public IIndexBuffer IndexBuffer { get; }
    }
}
