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
        public Color(float red, float green, float blue, float alpha = 1f)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }
    }
}
