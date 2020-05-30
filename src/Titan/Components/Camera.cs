using System.Numerics;
using System.Runtime.InteropServices;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Camera
    {
        public Matrix4x4 ViewProjection;
        public Matrix4x4 View;
        public Matrix4x4 Projection;
    }
}
