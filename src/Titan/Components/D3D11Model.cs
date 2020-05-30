using System.Runtime.InteropServices;
using Titan.Graphics.Buffers;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct D3D11Model
    {
        public IIndexBuffer IndexBuffer;
        public IVertexBuffer VertexBuffer;
    }
}