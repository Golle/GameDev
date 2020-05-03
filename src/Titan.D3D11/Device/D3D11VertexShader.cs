using System;

namespace Titan.D3D11.Device
{
    internal class D3D11VertexShader : ComPointer, ID3D11VertexShader
    {
        public D3D11VertexShader(IntPtr handle) : base(handle)
        {
        }
    }
}
