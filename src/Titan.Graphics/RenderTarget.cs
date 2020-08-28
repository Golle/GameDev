using System;
using Titan.D3D11.Device;

namespace Titan.Graphics
{
    internal class RenderTarget : IRenderTarget
    {
        private readonly ID3D11RenderTargetView _renderTarget;
        public IntPtr NativeHandle => _renderTarget.Handle;
        public RenderTarget(ID3D11RenderTargetView renderTarget)
        {
            _renderTarget = renderTarget;
        }

        public void Dispose()
        {
            _renderTarget.Dispose();
        }
    }
}
