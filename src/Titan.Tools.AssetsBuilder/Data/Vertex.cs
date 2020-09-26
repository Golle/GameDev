using System.Numerics;
using System.Runtime.InteropServices;

namespace Titan.Tools.AssetsBuilder.Data
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 Texture;
        public Color Color; // Temp
    }

    public struct Color
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public static readonly Color White = new Color {A = 1f, R = 1f, B = 1f, G = 1f};
    }
}
