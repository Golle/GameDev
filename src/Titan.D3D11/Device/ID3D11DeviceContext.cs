using System;

namespace Titan.D3D11.Device
{
    public interface ID3D11DeviceContext : IDisposable
    {

        void SetRenderTargets(ID3D11RenderTargetView renderTarget);
        void ClearRenderTargetView(ID3D11RenderTargetView renderTarget, in Color color);
    }
}
