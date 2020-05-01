using System;

namespace Titan.D3D11.Bindings.Models
{
    [Flags]
    public enum D3D11CpuAccessFlag : long
    {
        Write = 0x10000L,
        Read = 0x20000L
    }
}
