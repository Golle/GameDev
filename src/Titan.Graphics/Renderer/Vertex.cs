using System.Numerics;
using System.Runtime.InteropServices;
using Titan.D3D11;

namespace Titan.Graphics.Renderer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public float X;
        public float Y;
        public float Z;
        public Color Color;
    }
}
