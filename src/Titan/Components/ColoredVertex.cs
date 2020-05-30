using System.Numerics;
using System.Runtime.InteropServices;
using Titan.D3D11;
using Titan.Graphics.Layout;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct ColoredVertex
    {
        public Vector3 Position;
        public Color Color;

        public static VertexLayout VertexLayout = 
            new VertexLayout(2)
                .Append(nameof(Position), VertexLayoutTypes.Position3D)
                .Append(nameof(Color), VertexLayoutTypes.Float3Color);
    }
}
