using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct D3D11Texture2DDesc
    {
        public uint Width;
        public uint Height;
        public uint MipLevels;
        public uint ArraySize;
        public DxgiFormat Format;
        public DxgiSampleDesc SampleDesc;
        public D3D11Usage Usage;
        public D3D11BindFlag BindFlags;
        public D3D11CpuAccessFlag CPUAccessFlags;
        public uint MiscFlags;
    }
}
