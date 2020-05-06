using System;

namespace Titan.D3D11.Device
{
    internal class D3D11DepthStencilState : ComPointer, ID3D11DepthStencilState
    {
        public D3D11DepthStencilState(IntPtr handle) : base(handle)
        {
        }
    }
}