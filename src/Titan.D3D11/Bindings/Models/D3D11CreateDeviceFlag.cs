using System;

namespace Titan.D3D11.Bindings.Models
{
    [Flags]
    internal enum D3D11CreateDeviceFlag : uint
    {
        Default = 0x0,
        Singlethreaded = 0x1,
        Debug = 0x2,
        SwitchToRef = 0x4,
        PreventInternalThreadingOptimizations = 0x8,
        BgraSupport = 0x20,
        Debuggable = 0x40,
        PreventAlteringLayerSettingsFromRegistry = 0x80,
        DisableGpuTimeout = 0x100,
        VideoSupport = 0x800
    };
}
