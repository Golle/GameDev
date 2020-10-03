using System.Numerics;
using System.Runtime.InteropServices;

namespace Titan.Tools.AssetsBuilder.Data
{
    [StructLayout(LayoutKind.Sequential, Size = 256)]
    internal struct Header
    {
        public ushort Version;
        public int VertexSize;
        public int VertexCount;
        public int IndexSize;
        public int IndexCount;
        public int SubMeshCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SubMesh
    {
        public int StartIndex;
        public int Count;
        public int MaterialIndex;
        public Vector3 Min;
        public Vector3 Max;
    }
}
