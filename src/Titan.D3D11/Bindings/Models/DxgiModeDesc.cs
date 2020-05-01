using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DxgiModeDesc
    {
        public uint Width;
        public uint Height;
        public DxgiRational RefreshRate;
        public DxgiFormat Format;
        public DxgiModeScanlineOrder ScanlineOrdering;
        public DxgiModeScaling Scaling;
    }
}
