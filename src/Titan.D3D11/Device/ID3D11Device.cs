using System;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.InfoQueue;

namespace Titan.D3D11.Device
{
    public interface ID3D11Device : IDisposable
    {
        ID3D11SwapChain SwapChain { get; }
        ID3D11DeviceContext Context { get; }
        ID3D11RenderTargetView CreateRenderTargetView(ID3D11Resource resource);
        ID3D11InfoQueue CreateInfoQueue();
        ID3D11Buffer CreateBuffer(D3D11BufferDesc desc, D3D11SubresourceData? subresourceData = null);
        ID3D11VertexShader CreateVertexShader(ID3DBlob blob);
        ID3D11PixelShader CreatePixelShader(ID3DBlob blob);
        ID3D11InputLayout CreateInputLayout(in D3D11InputElementDesc[] desc, ID3DBlob blob);
    }
}
