using System.Numerics;
using System.Runtime.InteropServices;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Transform2D
    {
        public Vector2 Position;
        public Vector2 Scale;
        public float Rotation;


        internal Vector2 WorldPosition;
    }
}
