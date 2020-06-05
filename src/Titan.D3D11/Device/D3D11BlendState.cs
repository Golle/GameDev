using System;

namespace Titan.D3D11.Device
{
    internal class D3D11BlendState : ComPointer,  ID3D11BlendState
    {
        public D3D11BlendState(IntPtr handle) : base(handle)
        {
        }
    }
}
