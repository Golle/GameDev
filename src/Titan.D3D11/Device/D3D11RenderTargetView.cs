using System;

namespace Titan.D3D11.Device
{
    internal class D3D11RenderTargetView : ComPointer, ID3D11RenderTargetView
    {
        public D3D11RenderTargetView(IntPtr handle) : base(handle)
        {
        }
    }
}
