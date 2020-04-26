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
            DXGI_SWAP_CHAIN_DESC desc = default;
            desc.BufferCount = 1;
            desc.BufferDesc.Width= (uint) arguments.Window.Width;
            desc.BufferDesc.Height = (uint) arguments.Window.Height;
            desc.BufferDesc.Format = DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM;
            desc.BufferDesc.RefreshRate.Denominator = arguments.RefreshRate;
            desc.BufferDesc.Scaling = DXGI_MODE_SCALING.DXGI_MODE_SCALING_UNSPECIFIED;
            desc.BufferDesc.ScanlineOrdering = DXGI_MODE_SCANLINE_ORDER.DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED;
            
            desc.SampleDesc.Count = 1;
            desc.SampleDesc.Quality = 0;
            
            desc.Flags = 0;
            desc.BufferUsage = DXGI_USAGE.DXGI_USAGE_RENDER_TARGET_OUTPUT;
            desc.OutputWindow = arguments.Window.Handle;
            desc.Windowed = arguments.Window.Windowed ? 1 : 0;
            desc.SwapEffect = DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_DISCARD;
            desc.Flags = DXGI_SWAP_CHAIN_FLAG.DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH;

            HRESULT result;
            IntPtr swapChain;
            IntPtr device;
            D3D_FEATURE_LEVEL featureLevel;
            IntPtr context;
            unsafe
            {
                result = D3D11DeviceBindings.D3D11CreateDeviceAndSwapChain_(
                    arguments.Adapter,
                    D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_HARDWARE,
                    IntPtr.Zero,
                    0,
                    null,
                    0,
                    SdkVersion.GetVersion(),
                    in desc,
                    out swapChain,
                    out device,
                    out featureLevel,
                    out context);
            }
            if (result.Failed)
            {
                throw new Win32Exception((int)result.Code, "D3D11CreateDeviceAndSwapChain failed");
            }

            return new D3D11Device(device, featureLevel, new D3D11SwapChain(swapChain), new D3D11DeviceContext(context));
        }
    }
}
