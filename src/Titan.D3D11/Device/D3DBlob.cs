using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3DBlob : ComPointer, ID3DBlob
    {
        public D3DBlob(IntPtr handle) : base(handle)
        {
        }
        public UIntPtr GetBufferSize()
        {
            return D3D10BlobBindings.GetBufferSize_(Handle);
        }

        public IntPtr GetBufferPointer()
        {
            return D3D10BlobBindings.GetBufferPointer_(Handle);
        }
    }
}
