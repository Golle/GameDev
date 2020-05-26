using System.Numerics;
using System.Runtime.InteropServices;
using Titan.D3D11;

namespace Titan.Graphics.Renderer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position;
        public Color Color;
    }
}
