using System;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.InfoQueue;

namespace Titan.D3D11.Device
{
    public interface ID3D11Device : IDisposable
    {
        ID3D11SwapChain SwapChain { get; }
        ID3D11DeviceContext Context { get; }
        ID3D11RenderTargetView CreateRenderTargetView(ID3D11BackBuffer backBuffer);
        ID3D11InfoQueue CreateInfoQueue();
        ID3D11Buffer CreateBuffer(D3D11_BUFFER_DESC desc);
    }
}
