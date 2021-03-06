using System.Numerics;
using System.Runtime.InteropServices;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Transform3D
    {
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;

        internal Matrix4x4 ModelTransform;
        internal Matrix4x4 WorldTransform;
    }
}
