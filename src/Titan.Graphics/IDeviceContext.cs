using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;

namespace Titan.Graphics
{
    public interface IDeviceContext
    {
        void UpdateConstantBuffer<T>(IConstantBuffer<T> constantBuffer, in T data) where T : unmanaged;
        void UpdateVertexBuffer<T>(IVertexBuffer<T> vertexBuffer, in T[] data, int count) where T : unmanaged;
        void UpdateIndexBuffer(IIndexBuffer indexBuffer, in short[] data, int count);
        void SetVertexBuffer(IVertexBuffer vertexBuffer, uint startSlot = 0);
        void SetIndexBuffer(IIndexBuffer indexBuffer, uint offset = 0u);
        void DrawIndexed(uint numberOfIndices, uint startIndexLocation, int baseVertexLocation);
        void SetVertexShader(IVertexShader vertexShader);
        void SetPixelShader(IPixelShader pixelShader);
        void SetInputLayout(IInputLayout inputLayout);
        void SetPrimitiveTopology(PrimitiveTopology topology);
        void SetPixelShaderConstantBuffer(IConstantBuffer constantBuffer, uint startSlot = 0);
        void SetVertexShaderConstantBuffer(IConstantBuffer constantBuffer, uint startSlot = 0);
    }
}
