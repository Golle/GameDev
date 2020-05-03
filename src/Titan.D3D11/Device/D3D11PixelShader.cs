using System;

namespace Titan.D3D11.Device
{
    internal class D3D11PixelShader : ComPointer, ID3D11PixelShader
    {
        public D3D11PixelShader(IntPtr handle) : base(handle)
        {
        }
    }
}
