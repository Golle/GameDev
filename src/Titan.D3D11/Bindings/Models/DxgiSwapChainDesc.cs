using System;
using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DxgiSwapChainDesc
    {
        public DxgiModeDesc BufferDesc;
        public DxgiSampleDesc SampleDesc;
        public DxgiUsage BufferUsage;
        public uint BufferCount;
        public IntPtr OutputWindow;
        public int Windowed;
        public DxgiSwapEffect SwapEffect;
        public DxgiSwapChainFlag Flags;
    }
}
