using Titan.Graphics.Buffers;

namespace Titan.Graphics
{
    public interface IDeviceContext
    {
        void UpdateResourceData<T>(IResource resource, in T[] data, int count) where T : unmanaged;
        void SetVertexBuffer(IVertexBuffer vertexBuffer, uint startSlot = 0);
        void SetIndexBuffer(IIndexBuffer indexBuffer, uint offset = 0u);
        void DrawIndexed(uint numberOfIndices, uint startIndexLocation, int baseVertexLocation);
    }
}
