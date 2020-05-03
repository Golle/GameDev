using System;

namespace Titan.D3D11.Device
{
    internal class D3D11Buffer : ComPointer, ID3D11Buffer
    {
        public D3D11Buffer(IntPtr handle) : base(handle)
        {
        }
    }
}
