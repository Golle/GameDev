using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3D11RenderTargetView : ID3D11RenderTargetView
    {
        private readonly IntPtr _handle;
        public D3D11RenderTargetView(IntPtr handle)
        {
            _handle = handle;
        }

        public void Dispose()
        {
            CommonBindings.ReleaseComObject(_handle);
        }
    }
}
