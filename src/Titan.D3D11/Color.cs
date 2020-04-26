using System.Runtime.InteropServices;

namespace Titan.D3D11
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color
    {
        public float Red;
        public float Green;
        public float Blue;
        public float Alpha;
    }
}
