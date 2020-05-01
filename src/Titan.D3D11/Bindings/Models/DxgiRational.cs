using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DxgiRational
    {
        public uint Numerator;
        public uint Denominator;
    }
}
