using System;

namespace Titan.D3D11.Device
{
    internal class D3D11DepthStencilView : ComPointer, ID3D11DepthStencilView
    {
        public D3D11DepthStencilView(IntPtr handle) : base(handle)
        {
        }
    }
}