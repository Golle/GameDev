using System;
using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct D3D11SubresourceData
    {
        public unsafe void* pSysMem;
        public uint SysMemPitch;
        public uint SysMemSlicePitch;
    }
}
