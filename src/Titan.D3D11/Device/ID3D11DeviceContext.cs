using System;
using Titan.D3D11.Bindings.Models;

namespace Titan.D3D11.Device
{
    public interface ID3D11DeviceContext : IDisposable
    {
        void SetVertexBuffer(uint startSlot, ID3D11Buffer vertexBuffer, uint strides, uint offset);
        void Draw(uint vertexCount, uint startLocation);
        void SetRenderTargets(ID3D11RenderTargetView renderTarget);
        void ClearRenderTargetView(ID3D11RenderTargetView renderTarget, in Color color);
        void SetVertexShader(ID3D11VertexShader vertexShader);
        void SetPixelShader(ID3D11PixelShader pixelShader);
        void SetViewport(in D3D11Viewport viewport);
        void SetPrimitiveTopology(D3D11PrimitiveTopology topology);
        void SetInputLayout(ID3D11InputLayout inputLayout);
    }
}
