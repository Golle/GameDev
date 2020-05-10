using System;
using Titan.D3D11;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;

namespace Titan.Graphics.Stuff
{
    internal class Device : IDevice
    {
        private readonly ID3D11Device _device;
        private readonly ID3D11RenderTargetView _renderTarget;
        private readonly ID3D11DepthStencilView _depthStencilView;

        private readonly Color _clearColor = new Color{Blue = 0.1f, Alpha = 1f};

        private readonly bool _vSync = true;

        public Device(ID3D11Device device, ID3D11RenderTargetView renderTarget, ID3D11DepthStencilView depthStencilView)
        {
            _device = device ?? throw new ArgumentNullException(nameof(device));
            _renderTarget = renderTarget ?? throw new ArgumentNullException(nameof(renderTarget));
            _depthStencilView = depthStencilView ?? throw new ArgumentNullException(nameof(depthStencilView));
        }

        public IIndexBuffer CreateIndexBuffer()
        {
            throw new NotImplementedException();
        }

        public IVertexBuffer<T> CreateVertexBuffer<T>() where T : unmanaged, IVertex
        {
            throw new NotImplementedException();
        }

        public IConstantBuffer CrateConstantBuffer()
        {
            throw new NotImplementedException();
        }

        public void BeginRender()
        {
            _device.Context.ClearRenderTargetView(_renderTarget, _clearColor);
            _device.Context.ClearDepthStencilView(_depthStencilView, D3D11ClearFlag.Depth, 1f, 0);
        }

        public void EndRender()
        {
            _device.SwapChain.Present(_vSync);
        }

        public void Dispose()
        {
            _device.Dispose();
        }
    }
}
