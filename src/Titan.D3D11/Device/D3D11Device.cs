using System;
using System.ComponentModel;
using Titan.D3D11.Bindings;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.InfoQueue;
using Titan.Windows;

namespace Titan.D3D11.Device
{
    internal class D3D11Device : ID3D11Device
    {
        private readonly IntPtr _handle;
        public D3DFeatureLevel FeatureLevel { get; }
        public ID3D11SwapChain SwapChain { get; }
        public ID3D11DeviceContext Context { get; }

        public D3D11Device(IntPtr handle, D3DFeatureLevel featureLevel, ID3D11SwapChain swapChain, ID3D11DeviceContext context)
        {
            _handle = handle;
            FeatureLevel = featureLevel;
            SwapChain = swapChain;
            Context = context;
        }

        public ID3D11RenderTargetView CreateRenderTargetView(ID3D11Resource resource)
        {
            HRESULT result;
            IntPtr renderTargetView;
            unsafe
            {
                result = D3D11DeviceBindings.CreateRenderTargetView_(_handle, resource.Handle, (D3D11RenderTargetViewDesc*)null, out renderTargetView);
            }
            if (result.Failed)
            {
                throw new Win32Exception($"Device D3D11CreateRenderTargetView failed with code: 0x{result.Code.ToString("X")}");
            }
            return new D3D11RenderTargetView(renderTargetView);
        }

        public ID3D11InfoQueue CreateInfoQueue()
        {
            var result = D3D11CommonBindings.QueryInterface_(_handle, D3D11Resources.D3D11InfoQueue, out var infoQueue);
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateInfoQueue failed with code: 0x{result.Code.ToString("X")}");
            }

            return new D3D11InfoQueue(infoQueue);
        }

        public ID3D11Buffer CreateBuffer(in D3D11BufferDesc desc, D3D11SubresourceData? subresourceData = null)
        {
            HRESULT result;
            IntPtr buffer;
            unsafe
            {
                fixed (D3D11BufferDesc* bufferDesc = &desc)
                {
                    if (subresourceData.HasValue)
                    {
                        var subResource = subresourceData.Value;
                        result = D3D11DeviceBindings.CreateBuffer_(_handle, bufferDesc, &subResource, out buffer);
                    }
                    else
                    {
                        result = D3D11DeviceBindings.CreateBuffer_(_handle, bufferDesc, null, out buffer);
                    }
                }
            }
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateBuffer failed with code: 0x{result.Code.ToString("X")}");
            }
            return new D3D11Buffer(buffer);
        }

        public ID3D11VertexShader CreateVertexShader(ID3DBlob blob)
        {
            var result = D3D11DeviceBindings.CreateVertexShader_(_handle, blob.GetBufferPointer(), blob.GetBufferSize(), IntPtr.Zero, out var vertexShader);
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateVertexShader failed with code: 0x{result.Code.ToString("X")}");
            }

            return new D3D11VertexShader(vertexShader);
        }

        public ID3D11PixelShader CreatePixelShader(ID3DBlob blob)
        {
            var result = D3D11DeviceBindings.CreatePixelShader_(_handle, blob.GetBufferPointer(), blob.GetBufferSize(), IntPtr.Zero, out var pixelShader);
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreatePixelShader failed with code: 0x{result.Code.ToString("X")}");
            }

            return new D3D11PixelShader(pixelShader);
        }

        public ID3D11InputLayout CreateInputLayout(in D3D11InputElementDesc[] descs, ID3DBlob blob)
        {
            var result = D3D11DeviceBindings.CreateInputLayout_(_handle, descs, (uint)descs.Length, blob.GetBufferPointer(), blob.GetBufferSize(), out var inputLayout);
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateInputLayout failed with code: 0x{result.Code.ToString("X")}");
            }
            return new D3D11InputLayout(inputLayout);
        }

        public ID3D11DepthStencilState CreateDepthStencilState(in D3D11DepthStencilDesc depthDesc)
        {
            HRESULT result;
            IntPtr depthStencilState;
            unsafe
            {
                fixed (D3D11DepthStencilDesc* pointer = &depthDesc)
                {
                    result = D3D11DeviceBindings.CreateDepthStencilState_(_handle, pointer, out depthStencilState);
                }
            }
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateDepthStencilState failed with code: 0x{result.Code.ToString("X")}");
            }
            return new D3D11DepthStencilState(depthStencilState);
        }

        public ID3D11Texture2D CreateTexture2D(in D3D11Texture2DDesc texture2DDesc, D3D11SubresourceData? initialData = null)
        {
            HRESULT result;
            IntPtr texture2D;
            unsafe
            {
                fixed (D3D11Texture2DDesc* texture2DPointer = &texture2DDesc)
                {
                    if (initialData.HasValue)
                    {
                        var subResourceData = initialData.Value; // copies the value, maybe look at other ways to do this.
                        result = D3D11DeviceBindings.CreateTexture2D_(_handle, texture2DPointer, &subResourceData, out texture2D);
                    }
                    else
                    {
                        result = D3D11DeviceBindings.CreateTexture2D_(_handle, texture2DPointer, null, out texture2D);
                    }
                }
            }
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateTexture2D failed with code: 0x{result.Code.ToString("X")}");
            }
            return new D3D11Texture2D(texture2D);
        }

        public ID3D11DepthStencilView CreateDepthStencilView(ID3D11Resource resource, in D3D11DepthStencilViewDesc desc)
        {
            HRESULT result;
            IntPtr stencilView;
            unsafe
            {
                fixed (D3D11DepthStencilViewDesc* viewDescPointer = &desc)
                {
                    result = D3D11DeviceBindings.CreateDepthStencilView_(_handle, resource.Handle, viewDescPointer, out stencilView);
                }
            }
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateDepthStencilView failed with code: 0x{result.Code.ToString("X")}");
            }
            return new D3D11DepthStencilView(stencilView);
        }

        public void Dispose()
        {
            SwapChain.Dispose();
            Context.Dispose();
            D3D11CommonBindings.ReleaseComObject(_handle);
        }
    }
}
