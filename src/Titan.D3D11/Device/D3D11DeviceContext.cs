using System;
using Titan.D3D11.Bindings;
using Titan.D3D11.Bindings.Models;

namespace Titan.D3D11.Device
{
    internal class D3D11DeviceContext : ComPointer, ID3D11DeviceContext
    {
        public D3D11DeviceContext(IntPtr handle) 
            : base(handle)
        {
        }

        public void SetVertexBuffer(uint startSlot, ID3D11Buffer vertexBuffer, uint strides, uint offset)
        {
            D3D11DeviceContextBindings.IASetVertexBuffers_(Handle, startSlot, 1, vertexBuffer.Handle, strides, offset);
        }

        public void Draw(uint vertexCount, uint startLocation)
        {
            D3D11DeviceContextBindings.Draw_(Handle, vertexCount, startLocation);
        }

        public void OMSetRenderTargets(ID3D11RenderTargetView renderTarget, ID3D11DepthStencilView depthStencil)
        {
            unsafe
            {
                var renderTargetViews = stackalloc IntPtr[1];
                renderTargetViews[0] = renderTarget.Handle;
                D3D11DeviceContextBindings.OMSetRenderTargets_(Handle, 1, renderTargetViews, depthStencil?.Handle ?? IntPtr.Zero);
            }
        }

        public void ClearRenderTargetView(ID3D11RenderTargetView renderTarget, in Color color)
        {
            D3D11DeviceContextBindings.ClearRenderTargetView_(Handle, renderTarget.Handle, color);
        }

        public void ClearDepthStencilView(ID3D11DepthStencilView depthStencilView, D3D11ClearFlag clearFlags, float depth, sbyte stencil)
        {
            D3D11DeviceContextBindings.ClearDepthStencilView_(Handle,depthStencilView.Handle, clearFlags, depth, stencil);
        }

        public void SetVertexShader(ID3D11VertexShader vertexShader)
        {
            D3D11DeviceContextBindings.VSSetShader_(Handle, vertexShader.Handle, IntPtr.Zero, 0u);
        }

        public void SetPixelShader(ID3D11PixelShader pixelShader)
        {
            D3D11DeviceContextBindings.PSSetShader_(Handle, pixelShader.Handle, IntPtr.Zero, 0u);
        }

        public void SetViewport(in D3D11Viewport viewport)
        {
            unsafe
            {
                var viewports = stackalloc D3D11Viewport[1];
                viewports[0] = viewport;
                D3D11DeviceContextBindings.RSSetViewports_(Handle, 1, viewports);
            }
        }

        public void SetPrimitiveTopology(D3D11PrimitiveTopology topology)
        {
            D3D11DeviceContextBindings.IASetPrimitiveTopology_(Handle, topology);
        }

        public void SetInputLayout(ID3D11InputLayout inputLayout)
        {
            D3D11DeviceContextBindings.IASetInputLayout_(Handle, inputLayout.Handle);
        }

        public void SetIndexBuffer(ID3D11Buffer buffer, DxgiFormat format, uint offset)
        {
            D3D11DeviceContextBindings.IASetIndexBuffer_(Handle, buffer.Handle, format, offset);
        }

        public void DrawIndexed(uint indexCount, uint startIndexLocation, int baseVertexLocation)
        {
            D3D11DeviceContextBindings.DrawIndexed_(Handle, indexCount, startIndexLocation, baseVertexLocation);
        }

        public void VSSetConstantBuffer(uint startSlot, ID3D11Buffer constantBuffer)
        {
            D3D11DeviceContextBindings.VSSetConstantBuffers_(Handle, startSlot, 1, constantBuffer.Handle);
        }
        
        public void PSSetConstantBuffer(uint startSlot, ID3D11Buffer constantBuffer)
        {
            D3D11DeviceContextBindings.PSSetConstantBuffers_(Handle, startSlot, 1, constantBuffer.Handle);
        }

        public void PSSetShaderResources(uint startSlot, ID3D11ShaderResourceView resourceView)
        {
            D3D11DeviceContextBindings.PSSetShaderResources_(Handle, startSlot, 1, resourceView.Handle);
        }

        public void PSSetSamplers(uint startSlot, ID3D11SamplerState sampler)
        {
            D3D11DeviceContextBindings.PSSetSamplers_(Handle, startSlot, 1, sampler.Handle);
        }

        public void OMSetDepthStencilState(ID3D11DepthStencilState stencilState, uint stencilRef)
        {
            D3D11DeviceContextBindings.OMSetDepthStencilState_(Handle, stencilState.Handle, stencilRef);
        }

        public unsafe void UpdateSubresourceData(ID3D11Resource resource, void * data)
        {
            D3D11DeviceContextBindings.UpdateSubresource_(Handle, resource.Handle, 0, (D3D11Box*)null, data, 0,0);
        }

        public void OMSetBlendState(ID3D11BlendState blendState, Color blendFactor, uint sampleMask)
        {
            D3D11DeviceContextBindings.OMSetBlendState_(Handle, blendState.Handle, blendFactor, sampleMask);
        }

        public ID3D11CommandList FinishCommandList(bool restoreDeferredContextState = false)
        {
            var result = D3D11DeviceContextBindings.FinishCommandList_(Handle, restoreDeferredContextState, out var commandList);
            result.Check(nameof(D3D11DeviceContext));
            return new D3D11CommandList(commandList);
        }
        
        public void ExecuteCommandList(ID3D11CommandList commandList, bool restoreDeferredContextState = false)
        {
            D3D11DeviceContextBindings.ExecuteCommandList_(Handle, commandList.Handle, restoreDeferredContextState);
        }
    }
}
