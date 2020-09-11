using System;

namespace Titan.D3D11.Device
{
    internal class D3D11CommandList : ComPointer, ID3D11CommandList
    {
        public D3D11CommandList(IntPtr handle) 
            : base(handle)
        {
        }
    }
}
