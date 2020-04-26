using System;
using System.ComponentModel;
using Titan.D3D11.Bindings;
using Titan.D3D11.Bindings.Models;
using Titan.Windows;

namespace Titan.D3D11.Device
{
    internal class D3D11Device : ID3D11Device
    {
        private readonly IntPtr _handle;
        public D3D_FEATURE_LEVEL FeatureLevel { get; }
        public ID3D11SwapChain SwapChain { get; }
        public ID3D11DeviceContext Context { get; }

        public D3D11Device(IntPtr handle, D3D_FEATURE_LEVEL featureLevel, ID3D11SwapChain swapChain, ID3D11DeviceContext context)
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

        public void Dispose()
        {
            SwapChain.Dispose();
            Context.Dispose();
            CommonBindings.ReleaseComObject(_handle);
        }
    }
}
