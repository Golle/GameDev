using System;

namespace Titan.D3D11.Device
{
    internal class D3D11InputLayout : ComPointer, ID3D11InputLayout
    {
        public D3D11InputLayout(IntPtr handle) : base(handle)
        {
        }
    }
}
