using System.Numerics;
using System.Runtime.InteropServices;
using Titan.D3D11;

namespace Titan.Graphics.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct TexturedVertex
    {
        public Vector3 Position;
        public Vector3 Normals;
        public Vector2 Texture;
        public Color Color;
    }
}
