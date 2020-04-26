using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3D11SwapChain : ID3D11SwapChain
    {
        private readonly IntPtr _handle;

        public D3D11SwapChain(IntPtr handle)
        {
            _handle = handle;
        }

        public void Dispose()
        {
            CommonBindings.ReleaseComObject(_handle);
        }
    }
}
