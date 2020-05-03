using System;

namespace Titan.D3D11.Device
{
    internal class D3D11Resource : ComPointer, ID3D11Resource
    {
        public D3D11Resource(IntPtr handle) : base(handle)
        {
        }
    }
}
