﻿namespace Titan.D3D11.Bindings.Models
{
    internal enum DXGI_USAGE : uint
    {
        DXGI_CPU_ACCESS_NONE = 0,
        DXGI_CPU_ACCESS_DYNAMIC = 1,
        DXGI_CPU_ACCESS_READ_WRITE = 2,
        DXGI_CPU_ACCESS_SCRATCH = 3,
        DXGI_CPU_ACCESS_FIELD = 15,
        DXGI_USAGE_SHADER_INPUT = 0x00000010U,
        DXGI_USAGE_RENDER_TARGET_OUTPUT = (uint)(1L << (1 + 4)),
        DXGI_USAGE_BACK_BUFFER = 0x00000040U,
        DXGI_USAGE_SHARED = 0x00000080U,
        DXGI_USAGE_READ_ONLY = 0x00000100U,
        DXGI_USAGE_DISCARD_ON_PRESENT = 0x00000200U,
        DXGI_USAGE_UNORDERED_ACCESS = 0x00000400U,
    }
}
