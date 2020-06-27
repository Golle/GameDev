using Titan.Graphics.Buffers;

namespace Titan.Graphics.Models
{
    internal class Mesh : IMesh
    {
        public IVertexBuffer VertexBuffer { get; }
        public IIndexBuffer IndexBuffer { get; }
        public Mesh(IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer)
        {
            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
        }

        public void Dispose()
        {
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
        }
    }
}
