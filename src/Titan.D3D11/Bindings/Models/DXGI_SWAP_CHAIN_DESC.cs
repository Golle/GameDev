using System;
using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DXGI_SWAP_CHAIN_DESC
    {
        public DXGI_MODE_DESC BufferDesc;
        public DXGI_SAMPLE_DESC SampleDesc;
        public DXGI_USAGE BufferUsage;
        public uint BufferCount;
        public IntPtr OutputWindow;
        public int Windowed;
        public DXGI_SWAP_EFFECT SwapEffect;
        public DXGI_SWAP_CHAIN_FLAG Flags;
    }
}
