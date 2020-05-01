using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
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
            //D3D11_RENDER_TARGET_VIEW_DESC desc = default;
            unsafe
            {
                result = D3D11DeviceBindings.D3D11CreateRenderTargetView_(_handle, backBuffer.Handle, (D3D11_RENDER_TARGET_VIEW_DESC*)null, out renderTargetView);
                //result = D3D11DeviceBindings.D3D11CreateRenderTargetView_(_handle, backBuffer.Handle, &desc, out renderTargetView);
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
                var err = Marshal.GetLastWin32Error();
                throw new Win32Exception($"Device CreateInfoQueue failed with code: 0x{result.Code.ToString("X")}");
            }

            return new D3D11InfoQueue(infoQueue);
        }

        public ID3D11Buffer CreateBuffer(D3D11BufferDesc desc)
        {
            HRESULT result;
            IntPtr buffer = default;
            unsafe
            {
                result = D3D11DeviceBindings.D3D11CreateBuffer_(_handle, &desc, (D3D11_SUBRESOURCE_DATA*)null, out buffer);
            }
            return null;
        }

        public void Dispose()
        {
            SwapChain.Dispose();
            Context.Dispose();
            D3D11CommonBindings.ReleaseComObject(_handle);
        }
    }
}
