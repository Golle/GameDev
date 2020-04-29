using System;

namespace Titan.D3D11.Bindings.Models
{
    [Flags]
    internal enum D3D11_CREATE_DEVICE_FLAG : uint
    {
        D3D11_CREATE_DEVICE_DEFAULT = 0x0,
        D3D11_CREATE_DEVICE_SINGLETHREADED = 0x1,
        D3D11_CREATE_DEVICE_DEBUG = 0x2,
        D3D11_CREATE_DEVICE_SWITCH_TO_REF = 0x4,
        D3D11_CREATE_DEVICE_PREVENT_INTERNAL_THREADING_OPTIMIZATIONS = 0x8,
        D3D11_CREATE_DEVICE_BGRA_SUPPORT = 0x20,
        D3D11_CREATE_DEVICE_DEBUGGABLE = 0x40,
        D3D11_CREATE_DEVICE_PREVENT_ALTERING_LAYER_SETTINGS_FROM_REGISTRY = 0x80,
        D3D11_CREATE_DEVICE_DISABLE_GPU_TIMEOUT = 0x100,
        D3D11_CREATE_DEVICE_VIDEO_SUPPORT = 0x800
    };
}