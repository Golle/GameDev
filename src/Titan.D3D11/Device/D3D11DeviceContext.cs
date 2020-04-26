using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3D11DeviceContext : ID3D11DeviceContext
    {
        private readonly IntPtr _handle;

        public D3D11DeviceContext(IntPtr handle)
        {
            _handle = handle;
        }

        public void Dispose()
        {
            CommonBindings.ReleaseComObject(_handle);
        }
    }
}
