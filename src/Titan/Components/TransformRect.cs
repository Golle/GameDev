using System.Numerics;
using System.Runtime.InteropServices;
using Titan.Core.Math;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TransformRect
    {
        public Vector3 Position;
        public Size Size;
    }
}
