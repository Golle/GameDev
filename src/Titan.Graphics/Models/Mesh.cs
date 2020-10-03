using System.Numerics;
using Titan.Graphics.Buffers;

namespace Titan.Graphics.Models
{
    internal class Mesh : IMesh
    {
        public IVertexBuffer VertexBuffer { get; }
        public IIndexBuffer IndexBuffer { get; }
        public SubMesh[] SubSets { get; }
        public Mesh(IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer, in SubMesh[] subSets)
        {
            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
            SubSets = subSets;
        }

        public void Dispose()
        {
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
        }
    }
}
