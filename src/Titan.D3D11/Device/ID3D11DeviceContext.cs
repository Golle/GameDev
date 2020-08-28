using System;
using Titan.D3D11.Bindings.Models;

namespace Titan.D3D11.Device
{
    public interface ID3D11DeviceContext : IDisposable
    {
        void SetVertexBuffer(uint startSlot, ID3D11Buffer vertexBuffer, uint strides, uint offset);
        void SetVertexBuffer(uint startSlot, IntPtr vertexBuffer, uint strides, uint offset);
        void Draw(uint vertexCount, uint startLocation);
        void OMSetRenderTargets(ID3D11RenderTargetView renderTarget, ID3D11DepthStencilView depthStencil);
        void ClearRenderTargetView(ID3D11RenderTargetView renderTarget, in Color color);
        void ClearRenderTargetView(IntPtr renderTarget, in Color color);
        void ClearDepthStencilView(ID3D11DepthStencilView depthStencilView, D3D11ClearFlag clearFlags, float depth, sbyte stencil);
        void ClearDepthStencilView(IntPtr depthStencilView, D3D11ClearFlag clearFlags, float depth, sbyte stencil);
        void SetVertexShader(ID3D11VertexShader vertexShader);
        void SetVertexShader(IntPtr vertexShader);
        void SetPixelShader(ID3D11PixelShader pixelShader);
        void SetPixelShader(IntPtr pixelShader);
        void SetViewport(in D3D11Viewport viewport);
        void SetPrimitiveTopology(D3D11PrimitiveTopology topology);
        void SetInputLayout(ID3D11InputLayout inputLayout);
        void SetInputLayout(IntPtr inputLayout);
        void SetIndexBuffer(ID3D11Buffer buffer, DxgiFormat format, uint offset);
        void SetIndexBuffer(IntPtr buffer, DxgiFormat format, uint offset);
        void DrawIndexed(uint indexCount, uint startIndexLocation, int baseVertexLocation);
        void VSSetConstantBuffer(uint startSlot, ID3D11Buffer constantBuffer);
        void VSSetConstantBuffer(uint startSlot, IntPtr constantBuffer);
        void PSSetConstantBuffer(uint startSlot, ID3D11Buffer constantBuffer);
        void PSSetConstantBuffer(uint startSlot, IntPtr constantBuffer);
        void PSSetShaderResources(uint startSlot, ID3D11ShaderResourceView resourceView);
        void PSSetShaderResources(uint startSlot, IntPtr resourceView);
        void PSSetSamplers(uint startSlot, ID3D11SamplerState sampler);
        void PSSetSamplers(uint startSlot, IntPtr sampler);
        void OMSetDepthStencilState(ID3D11DepthStencilState stencilState, uint stencilRef);
        unsafe void UpdateSubresourceData(ID3D11Resource resource, void * data);
        unsafe void UpdateSubresourceData(IntPtr resource, void * data);
        void UpdateSubresourceData<T>(IntPtr resource, in T data) where T : unmanaged;
        void UpdateSubresourceData<T>(IntPtr resource, in T[] data) where T : unmanaged;
        void OMSetBlendState(ID3D11BlendState blendState, in Color blendFactor, uint sampleMask);
        void OMSetBlendState(IntPtr blendState, in Color blendFactor, uint sampleMask);
        ID3D11CommandList FinishCommandList(bool restoreDeferredContextState = false);
        void ExecuteCommandList(ID3D11CommandList commandList, bool restoreDeferredContextState = false);
    }
}
