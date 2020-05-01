using System;

namespace Titan.D3D11.Bindings.Models
{
    [Flags]
    public enum D3D11CpuAccessFlag : uint
    {
        Unspecified = 0,
        Write = 0x10000,
        Read = 0x20000
    }
}
