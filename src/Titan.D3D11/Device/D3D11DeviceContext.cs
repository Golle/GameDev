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

        public void SetVertexBuffer(uint startSlot, ID3D11Buffer vertexBuffer, uint strides, uint offset) => SetVertexBuffer(startSlot, vertexBuffer.Handle, strides, offset);
        public void SetVertexBuffer(uint startSlot, IntPtr vertexBuffer, uint strides, uint offset) => D3D11DeviceContextBindings.IASetVertexBuffers_(Handle, startSlot, 1, vertexBuffer, strides, offset);

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
                OMSetRenderTargets(renderTargetViews, 1, depthStencil?.Handle ?? IntPtr.Zero);
            }
        }

        public unsafe void OMSetRenderTargets(IntPtr* renderTargets, uint numberOfRenderTargets, IntPtr depthStencil)
        {
            D3D11DeviceContextBindings.OMSetRenderTargets_(Handle, numberOfRenderTargets, renderTargets, depthStencil);
        }

        public void ClearRenderTargetView(ID3D11RenderTargetView renderTarget, in Color color) => ClearRenderTargetView(renderTarget.Handle, color);
        public void ClearRenderTargetView(IntPtr renderTarget, in Color color) => D3D11DeviceContextBindings.ClearRenderTargetView_(Handle, renderTarget, color);
        public void ClearDepthStencilView(ID3D11DepthStencilView depthStencilView, D3D11ClearFlag clearFlags, float depth, sbyte stencil) => ClearDepthStencilView(depthStencilView.Handle, clearFlags, depth, stencil);
        public void ClearDepthStencilView(IntPtr depthStencilView, D3D11ClearFlag clearFlags, float depth, sbyte stencil) => D3D11DeviceContextBindings.ClearDepthStencilView_(Handle, depthStencilView, clearFlags, depth, stencil);

        public void SetVertexShader(ID3D11VertexShader vertexShader) => SetVertexShader(vertexShader.Handle);
        public void SetVertexShader(IntPtr vertexShader) => D3D11DeviceContextBindings.VSSetShader_(Handle, vertexShader, IntPtr.Zero, 0u);
        public void SetPixelShader(ID3D11PixelShader pixelShader) => SetPixelShader(pixelShader.Handle);
        public void SetPixelShader(IntPtr pixelShader) => D3D11DeviceContextBindings.PSSetShader_(Handle, pixelShader, IntPtr.Zero, 0u);

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

        public void SetInputLayout(ID3D11InputLayout inputLayout) => SetInputLayout(inputLayout.Handle);
        public void SetInputLayout(IntPtr inputLayout) => D3D11DeviceContextBindings.IASetInputLayout_(Handle, inputLayout);
        public void DrawIndexed(uint indexCount, uint startIndexLocation, int baseVertexLocation) => D3D11DeviceContextBindings.DrawIndexed_(Handle, indexCount, startIndexLocation, baseVertexLocation);
        public void SetIndexBuffer(ID3D11Buffer buffer, DxgiFormat format, uint offset) => SetIndexBuffer(buffer.Handle, format, offset);
        public void SetIndexBuffer(IntPtr buffer, DxgiFormat format, uint offset) => D3D11DeviceContextBindings.IASetIndexBuffer_(Handle, buffer, format, offset);
        public void VSSetConstantBuffer(uint startSlot, ID3D11Buffer constantBuffer) => VSSetConstantBuffer(startSlot, constantBuffer.Handle);
        public void VSSetConstantBuffer(uint startSlot, IntPtr constantBuffer) => D3D11DeviceContextBindings.VSSetConstantBuffers_(Handle, startSlot, 1, constantBuffer);

        public void VSSetShaderResources(uint startSlot, IntPtr resourceView)
        {
            unsafe
            {
                var resources = stackalloc IntPtr[1];
                resources[0] = resourceView;
                D3D11DeviceContextBindings.VSSetShaderResources_(Handle, 0, 1, resources);
            }
        }

        public void VSSetShaderResources(uint startSlot, ID3D11ShaderResourceView[] resourceViews)
        {
            unsafe
            {
                var count = resourceViews.Length;
                var shaderResources = stackalloc IntPtr[count];
                for (var i = 0; i < count; ++i)
                {
                    shaderResources[i] = resourceViews[i].Handle;
                }
                VSSetShaderResources(startSlot, shaderResources, (uint) count);
            }
        }

        public unsafe void VSSetShaderResources(uint startSlot, IntPtr* resources, uint count)
        {
            D3D11DeviceContextBindings.VSSetShaderResources_(Handle, 0, count, resources);
        }

        public void VSSetShaderResource(uint startSlot, ID3D11ShaderResourceView resourceView)
        {
            unsafe
            {
                var shaderResources = stackalloc IntPtr[1];
                shaderResources[0] = resourceView.Handle;
                D3D11DeviceContextBindings.VSSetShaderResources_(Handle, 0, 1, shaderResources);
            }
        } 
        public void PSSetConstantBuffer(uint startSlot, ID3D11Buffer constantBuffer) => PSSetConstantBuffer(startSlot, constantBuffer.Handle);
        public void PSSetConstantBuffer(uint startSlot, IntPtr constantBuffer) => D3D11DeviceContextBindings.PSSetConstantBuffers_(Handle, startSlot, 1, constantBuffer);
        public void PSSetShaderResources(uint startSlot, ID3D11ShaderResourceView resourceView) => PSSetShaderResources(startSlot, resourceView.Handle);
        public void PSSetShaderResources(uint startSlot, IntPtr resourceView)
        {
            unsafe
            {
                var resources = stackalloc IntPtr[1];
                resources[0] = resourceView;
                PSSetShaderResources(startSlot, resources, 1);
            }
        }

        public unsafe void PSSetShaderResources(uint startSlot, IntPtr* resources, uint count) => D3D11DeviceContextBindings.PSSetShaderResources_(Handle, startSlot, count, resources);

        public void PSSetSamplers(uint startSlot, ID3D11SamplerState sampler) => PSSetSamplers(startSlot, sampler.Handle);
        public void PSSetSamplers(uint startSlot, IntPtr sampler) => D3D11DeviceContextBindings.PSSetSamplers_(Handle, startSlot, 1, sampler);

        public void OMSetDepthStencilState(ID3D11DepthStencilState stencilState, uint stencilRef)
        {
            D3D11DeviceContextBindings.OMSetDepthStencilState_(Handle, stencilState.Handle, stencilRef);
        }

        public unsafe void UpdateSubresourceData(ID3D11Resource resource, void * data) => UpdateSubresourceData(resource.Handle, data);
        public unsafe void UpdateSubresourceData(IntPtr resource, void* data) => D3D11DeviceContextBindings.UpdateSubresource_(Handle, resource, 0, (D3D11Box*)null, data, 0, 0);
        public void UpdateSubresourceData<T>(IntPtr resource, in T data) where T : unmanaged
        {
            unsafe
            {
                fixed (void* ptr = &data)
                {
                    D3D11DeviceContextBindings.UpdateSubresource_(Handle, resource, 0, null, ptr, 0, 0);
                }
            }
        }

        public void UpdateSubresourceData<T>(IntPtr resource, in T[] data) where T : unmanaged
        {
            unsafe
            {
                fixed (void* ptr = data)
                {
                    D3D11DeviceContextBindings.UpdateSubresource_(Handle, resource, 0, null, ptr, 0, 0);
                }
            }
        }

        public void OMSetBlendState(ID3D11BlendState blendState, in Color blendFactor, uint sampleMask) => OMSetBlendState(blendState.Handle, blendFactor, sampleMask);
        public void OMSetBlendState(IntPtr blendState, in Color blendFactor, uint sampleMask) => D3D11DeviceContextBindings.OMSetBlendState_(Handle, blendState, blendFactor, sampleMask);

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

        public void Map(IntPtr resource, D3D11Map mapType, out D3D11MappedSubresource mappedResource)
        {
            var result = D3D11DeviceContextBindings.Map_(Handle, resource, 0, mapType, 0, out mappedResource);
            result.Check(nameof(D3D11DeviceContext));
        }

        public void Unmap(IntPtr resource)
        {
            D3D11DeviceContextBindings.Unmap_(Handle, resource, 0);
        }
    }
}
