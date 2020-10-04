using System.Runtime.InteropServices;

namespace Titan.Tools.AssetsBuilder.Data
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Pixel
    {
        public byte Red;
        public byte Green;
        public byte Blue;
        public byte Alpha;
    }
}
