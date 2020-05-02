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

        public ID3D11RenderTargetView CreateRenderTargetView(ID3D11BackBuffer backBuffer)
        {
            HRESULT result;
            IntPtr renderTargetView;
            unsafe
            {
                result = D3D11DeviceBindings.D3D11CreateRenderTargetView_(_handle, backBuffer.Handle, (D3D11RenderTargetViewDesc*)null, out renderTargetView);
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

        public ID3D11Buffer CreateBuffer(D3D11BufferDesc desc, D3D11SubresourceData? subresourceData = null)
        {
            HRESULT result;
            IntPtr buffer;
            unsafe
            {
                if (subresourceData.HasValue)
                {
                    var subResource = subresourceData.Value;
                    result = D3D11DeviceBindings.D3D11CreateBuffer_(_handle, &desc, &subResource, out buffer);
                }
                else
                {
                    result = D3D11DeviceBindings.D3D11CreateBuffer_(_handle, &desc, null, out buffer);
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
            var result = D3D11DeviceBindings.D3D11CreateVertexShader_(_handle, blob.GetBufferPointer(), blob.GetBufferSize(), IntPtr.Zero, out var vertexShader);
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateVertexShader failed with code: 0x{result.Code.ToString("X")}");
            }

            return new D3D11VertexShader(vertexShader);
        }

        public ID3D11PixelShader CreatePixelShader(ID3DBlob blob)
        {
            var result = D3D11DeviceBindings.D3D11CreatePixelShader_(_handle, blob.GetBufferPointer(), blob.GetBufferSize(), IntPtr.Zero, out var pixelShader);
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreatePixelShader failed with code: 0x{result.Code.ToString("X")}");
            }

            return new D3D11PixelShader(pixelShader);
        }

        public ID3D11InputLayout CreateInputLayout(in D3D11InputElementDesc[] descs, ID3DBlob blob)
        {
            var result = D3D11DeviceBindings.D3D11CreateInputLayout_(_handle, descs, (uint)descs.Length, blob.GetBufferPointer(), blob.GetBufferSize(), out var inputLayout);
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateInputLayout failed with code: 0x{result.Code.ToString("X")}");
            }
            return new D3D11InputLayout(inputLayout);
        }

        public void Dispose()
        {
            SwapChain.Dispose();
            Context.Dispose();
            D3D11CommonBindings.ReleaseComObject(_handle);
        }
    }
}
