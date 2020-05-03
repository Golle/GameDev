using System;

namespace Titan.D3D11.Device
{
    public interface ID3D11SwapChain : IDisposable
    {
        ID3D11Resource GetBuffer(uint buffer, Guid riid);
        void Present(bool vSync = true);
    }
}
