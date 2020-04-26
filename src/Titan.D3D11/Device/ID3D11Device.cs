using System;

namespace Titan.D3D11.Device
{
    public interface ID3D11Device : IDisposable
    {
        ID3D11SwapChain SwapChain { get; }
        ID3D11DeviceContext Context { get; }
    }
}
