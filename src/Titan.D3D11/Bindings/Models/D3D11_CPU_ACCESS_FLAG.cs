using System;

namespace Titan.D3D11.Bindings.Models
{
    [Flags]
    public enum D3D11_CPU_ACCESS_FLAG : long
    {
        D3D11_CPU_ACCESS_WRITE = 0x10000L,
        D3D11_CPU_ACCESS_READ = 0x20000L
    }
}
