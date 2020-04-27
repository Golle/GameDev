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
            D3D11CommonBindings.ReleaseComObject(_handle);
        }

        public void SetRenderTargets(ID3D11RenderTargetView renderTarget)
        {
            D3D11DeviceContextBindings.DeviceContextOMSetRenderTargets_(_handle, 1, new[]{renderTarget.Handle}, IntPtr.Zero);
        }

        public void ClearRenderTargetView(ID3D11RenderTargetView renderTarget, in Color color)
        {
            D3D11DeviceContextBindings.ClearRenderTargetView(_handle, renderTarget.Handle, color);
        }
    }
}
