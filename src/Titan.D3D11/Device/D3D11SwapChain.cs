using System;
using System.ComponentModel;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3D11SwapChain : ComPointer, ID3D11SwapChain
    {
        public D3D11SwapChain(IntPtr handle) 
            : base(handle)
        {
        }

        public ID3D11Resource GetBuffer(uint buffer, Guid riid)
        {
            var result = D3D11SwapChainBindings.GetBuffer(Handle, buffer, riid, out var backBuffer);
            if (result.Failed)
            {
                throw new Win32Exception((int)result.Code, "SwapChain GetBuffer failed");
            }
            return new D3D11Resource(backBuffer);
        }

        public void Present(bool vSync = true)
        {
            D3D11SwapChainBindings.Present(Handle, vSync ? 1u : 0u, 0);
        }
    }
}
