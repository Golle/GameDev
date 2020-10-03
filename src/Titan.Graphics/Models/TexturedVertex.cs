using System.Numerics;
using System.Runtime.InteropServices;

namespace Titan.Graphics.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct TexturedVertex
    {
        public Vector3 Position;
        public Vector3 Normals;
        public Vector2 Texture;
    }
}
