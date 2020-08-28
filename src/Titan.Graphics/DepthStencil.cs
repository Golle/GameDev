using System;
using Titan.D3D11.Device;

namespace Titan.Graphics
{
    internal class DepthStencil : IDepthStencil
    {
        private readonly ID3D11DepthStencilView _depStencilView;
        public IntPtr NativeHandle => _depStencilView.Handle;
        public DepthStencil(ID3D11DepthStencilView depStencilView)
        {
            _depStencilView = depStencilView;
        }

        public void Dispose()
        {
            _depStencilView.Dispose();
        }
    }
}
