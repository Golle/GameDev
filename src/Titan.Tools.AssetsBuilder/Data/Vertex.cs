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
    }
}
