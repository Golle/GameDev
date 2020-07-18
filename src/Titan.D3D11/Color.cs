using System.Runtime.InteropServices;

namespace Titan.D3D11
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color(float color)
        {
            R = G = B = A = color;
        }
        public Color(float r, float g, float b, float a = 1f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }


        public static readonly Color White = new Color(1);
        public static readonly Color Red = new Color(1f, 0, 0);
        public static readonly Color Blue = new Color(0, 0, 1f);
        public static readonly Color Green = new Color(0, 1f, 0);
    }
}
