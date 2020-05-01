using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3DBlob : ID3DBlob
    {
        private readonly IntPtr _handle;
        public D3DBlob(IntPtr handle)
        {
            _handle = handle;
        }
        public UIntPtr GetBufferSize()
        {
            return D3D10BlobBindings.GetBufferSize_(_handle);
        }

        public IntPtr GetBufferPointer()
        {
            return D3D10BlobBindings.GetBufferPointer_(_handle);
        }

        public void Dispose()
        {
            D3D11CommonBindings.ReleaseComObject(_handle);
        }
    }
}
