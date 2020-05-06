using System;

namespace Titan.D3D11.Device
{
    internal class D3D11Texture2D : ComPointer, ID3D11Texture2D
    {
        public D3D11Texture2D(IntPtr handle) : base(handle)
        {
        }
    }
}