using System;
using System.Numerics;
using Titan.Graphics.Buffers;

namespace Titan.Graphics.Models
{
    public interface IMesh : IDisposable
    {
        public IVertexBuffer VertexBuffer { get; }
        public IIndexBuffer IndexBuffer { get; }
        public Vector3 Min { get; }
        public Vector3 Max { get; }
    }
}
