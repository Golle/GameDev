using System;
using Titan.D3D11;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Compiler;
using Titan.D3D11.Device;
using Titan.Windows.Window;

namespace Titan.Graphics
{
    internal class DisplayFactory : IDisplayFactory
    {
        private readonly IWindowCreator _windowCreator;
        private readonly ID3D11DeviceFactory _d3D11DeviceFactory;
        private readonly ID3DCompiler _compiler;

        public DisplayFactory(IWindowCreator windowCreator, ID3D11DeviceFactory d3D11DeviceFactory, ID3DCompiler compiler)
        {
            _windowCreator = windowCreator;
            _d3D11DeviceFactory = d3D11DeviceFactory;
            _compiler = compiler;
        }
        public IDisplay Create(string title, int width, int height)
        {
            return Create(title, width, height, null);
        }
     
        public IDisplay Create(string title, int width, int height, IAdapter adapter)
        {
            const uint defaultRefreshRate = 144;
            const bool debug = true;

            var window = _windowCreator.CreateWindow(new CreateWindowArguments(title, width, height));

            // Create device
            var d3D11Device = _d3D11DeviceFactory.Create(new CreateDeviceArguments(window, defaultRefreshRate, adapter?.Handle ?? IntPtr.Zero, debug));
            
            // Created the backbuffer and the RenderTargetView
            using var backBuffer = d3D11Device.SwapChain.GetBuffer(0, D3D11Resources.D3D11Resource);
            var renderTarget = d3D11Device.CreateRenderTargetView(backBuffer);


            D3D11Texture2DDesc sTarget = default;
            sTarget.Height = (uint)window.Height;
            sTarget.Width = (uint)window.Width;
            sTarget.MipLevels = 1u;
            sTarget.ArraySize = 1u;
            sTarget.Format = DxgiFormat.R8G8B8A8Unorm;
            sTarget.SampleDesc.Count = 1;
            sTarget.SampleDesc.Quality = 0;
            sTarget.Usage = D3D11Usage.Default;
            sTarget.BindFlags = D3D11BindFlag.RenderTarget;

            //var d3D11Texture2D = d3D11Device.CreateTexture2D(sTarget); // TODO: this is getting leaked
            //var secondRenderTarget = d3D11Device.CreateRenderTargetView(d3D11Texture2D); // TODO: this is getting leaked


            // Create the DepthStencilView (z-buffering)
            D3D11DepthStencilDesc depthDesc = default;
            depthDesc.DepthEnable = true;
            //depthDesc.DepthWriteMask = D3D11DepthWriteMask.Zero;
            //depthDesc.DepthFunc = D3D11ComparisonFunc.LessEqual;  // this is needed to add alpha-blending to sprites, not sure why.
            depthDesc.DepthWriteMask = D3D11DepthWriteMask.All;
            depthDesc.DepthFunc = D3D11ComparisonFunc.LessEqual;  // this is needed to add alpha-blending to sprites, not sure why.
            using var depthStencilState = d3D11Device.CreateDepthStencilState(depthDesc);
            d3D11Device.Context.OMSetDepthStencilState(depthStencilState, 1u);

            D3D11Texture2DDesc texture2DDesc = default;
            texture2DDesc.Height = (uint)window.Height;
            texture2DDesc.Width = (uint)window.Width;
            texture2DDesc.MipLevels = 1u;
            texture2DDesc.ArraySize = 1u;
            texture2DDesc.Format = DxgiFormat.D32Float;
            //texture2DDesc.SampleDesc.Count = 4u;  // AA
            texture2DDesc.SampleDesc.Count = 1;
            texture2DDesc.SampleDesc.Quality = 0;
            texture2DDesc.Usage = D3D11Usage.Default;
            texture2DDesc.BindFlags = D3D11BindFlag.DepthStencil;
            using var depthStencil = d3D11Device.CreateTexture2D(texture2DDesc);

            D3D11DepthStencilViewDesc viewDesc = default;
            viewDesc.Format = DxgiFormat.D32Float;
            viewDesc.ViewDimension = D3D11DsvDimension.Texture2D;
            //viewDesc.ViewDimension = D3D11DsvDimension.Texture2Dms; // AA
            viewDesc.Texture2D.MipSlice = 0u;
            var depthStencilView = d3D11Device.CreateDepthStencilView(depthStencil, viewDesc);

            // Set the render target view and depthStencil
            unsafe
            {
                var renderTargets = stackalloc IntPtr[1];
                renderTargets[0] = renderTarget.Handle;
                d3D11Device.Context.OMSetRenderTargets(renderTargets, 1, depthStencilView.Handle);
            }
            


            // Set the default viewport
            D3D11Viewport viewport = default;
            viewport.Width = window.Width;
            viewport.Height = window.Height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            d3D11Device.Context.SetViewport(viewport);


            // Create the device abstraction
            var device = new Device(d3D11Device, renderTarget, depthStencilView, _compiler);

            return new Display(device, window);
        }
    }
}
