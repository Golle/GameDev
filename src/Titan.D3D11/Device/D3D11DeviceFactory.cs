using System;
using System.ComponentModel;
using Titan.D3D11.Bindings;
using Titan.D3D11.Bindings.Models;
using Titan.Windows;

namespace Titan.D3D11.Device
{
    public class D3D11DeviceFactory : ID3D11DeviceFactory
    {
        public ID3D11Device Create(CreateDeviceArguments arguments)
        {
            DxgiSwapChainDesc desc = default;
            desc.BufferCount = 2;
            desc.BufferDesc.Width= (uint) arguments.Window.Width;
            desc.BufferDesc.Height = (uint) arguments.Window.Height;
            desc.BufferDesc.Format = DxgiFormat.R8G8B8A8Unorm;
            desc.BufferDesc.RefreshRate.Denominator = arguments.RefreshRate;
            desc.BufferDesc.Scaling = DxgiModeScaling.Unspecified;
            desc.BufferDesc.ScanlineOrdering = DxgiModeScanlineOrder.Unspecified;
            
            desc.SampleDesc.Count = 1;
            desc.SampleDesc.Quality = 0;
            
            desc.Flags = 0;
            desc.BufferUsage = DxgiUsage.RenderTargetOutput;
            desc.OutputWindow = arguments.Window.Handle;
            desc.Windowed = arguments.Window.Windowed ? 1 : 0;
            desc.SwapEffect = DxgiSwapEffect.FlipDiscard;
            desc.Flags = DxgiSwapChainFlag.AllowModeSwitch;

            HRESULT result;
            IntPtr swapChain;
            IntPtr device;
            D3DFeatureLevel featureLevel;
            IntPtr context;
            var flags = arguments.Debug ? D3D11CreateDeviceFlag.Debug : D3D11CreateDeviceFlag.Default;

            unsafe
            {
                result = D3D11DeviceBindings.D3D11CreateDeviceAndSwapChain_(
                    arguments.Adapter,
                    D3DDriverType.Hardware,
                    IntPtr.Zero,
                    flags,
                    null,
                    0,
                    D3D11CommonBindings.D3D11SdkVersion(),
                    in desc,
                    out swapChain,
                    out device,
                    out featureLevel,
                    out context);
            }
            if (result.Failed)
            {
                throw new Win32Exception($"D3D11CreateDeviceAndSwapChain failed with code : 0x{ result.Code.ToString("X")}");
            }

            return new D3D11Device(device, featureLevel, new D3D11SwapChain(swapChain), new D3D11DeviceContext(context));
        }
    }
}
