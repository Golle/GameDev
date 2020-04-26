using System;
using System.ComponentModel;
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

        public ID3D11BackBuffer GetBuffer(uint buffer, Guid riid)
        {
            var result = D3D11SwapChainBindings.GetBuffer(_handle, buffer, riid, out var backBuffer);
            if (result.Failed)
            {
                throw new Win32Exception((int)result.Code, "SwapChain GetBuffer failed");
            }
            return new D3D11BackBuffer(backBuffer);
        }

        public void Present()
        {
            D3D11SwapChainBindings.Present(_handle, 1, 0);
        }
    }
}
