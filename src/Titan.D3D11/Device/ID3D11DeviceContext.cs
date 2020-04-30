using System;

namespace Titan.D3D11.Device
{
    public interface ID3D11DeviceContext : IDisposable
    {
        void SetVertexBuffers(uint startSlot, ID3D11Buffer[] vertexBuffers, uint strides);
        void Draw(uint vertexCount, uint startLocation);
        void SetRenderTargets(ID3D11RenderTargetView renderTarget);
        void ClearRenderTargetView(ID3D11RenderTargetView renderTarget, in Color color);
    }

    public interface ID3D11Buffer
    {
    }
}
