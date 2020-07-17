using System.Numerics;
using Titan.Graphics.Buffers;

namespace Titan.Graphics.Models
{
    internal class Mesh : IMesh
    {
        public IVertexBuffer VertexBuffer { get; }
        public IIndexBuffer IndexBuffer { get; }
        public Vector3 Min { get; }
        public Vector3 Max { get; }
        public Mesh(IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer, in Vector3 min, in Vector3 max)
        {
            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
            Min = min;
            Max = max;
        }

        public void Dispose()
        {
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
        }
    }
}
