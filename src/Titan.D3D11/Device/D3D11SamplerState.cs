using System;

namespace Titan.D3D11.Device
{
    internal class D3D11SamplerState : ComPointer, ID3D11SamplerState
    {
        public D3D11SamplerState(IntPtr handle) 
            : base(handle)
        {
        }
    }
}
