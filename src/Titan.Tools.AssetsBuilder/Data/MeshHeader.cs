using System.Runtime.InteropServices;

namespace Titan.Tools.AssetsBuilder.Data
{
    [StructLayout(LayoutKind.Sequential, Size = 8)]
    internal struct MeshHeader
    {
        public int VerticeCount;
        public int IndexCount;
    }
}
