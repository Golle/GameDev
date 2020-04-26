using System;
using Titan.D3D11.Bindings;
using Titan.D3D11.Bindings.Models;

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

        public void Dispose()
        {
            SwapChain.Dispose();
            Context.Dispose();
            var res = CommonBindings.ReleaseComObject(_handle);
        }
    }
}
