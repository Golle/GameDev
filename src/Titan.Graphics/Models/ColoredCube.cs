using System.Numerics;
using System.Runtime.InteropServices;
using Titan.D3D11;

namespace Titan.Graphics.Models
{
    public class ColoredCube
    {
        
        
        [StructLayout(LayoutKind.Sequential)]
        public struct Vertex
        {
            public Vector3 Position;
            public Color Color;
        }

        public static Vertex[] Create(Color color)
        {
            return new[]
            {
                new Vertex {Position = new Vector3(1, 0, 0), Color = new Color(1f, 0, 0)}
            };
        }
    }
}
