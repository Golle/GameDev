using System;

namespace Titan.D3D11.Device
{
    internal class D3D11ShaderResourceView : ComPointer, ID3D11ShaderResourceView
    {
        public D3D11ShaderResourceView(IntPtr handle)
            : base(handle)
        {
        }
    }
}
