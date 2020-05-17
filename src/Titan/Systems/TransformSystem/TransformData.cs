using System.Numerics;
using System.Runtime.InteropServices;

namespace Titan.Systems.TransformSystem
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TransformData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        //public Vector3 Pivot; // might use later
        public Matrix4x4 LocalTransform;
        public Matrix4x4 WorldTransform;

        internal void Update(in Matrix4x4? world)
        {
            LocalTransform =
                Matrix4x4.CreateTranslation(Position) *
                Matrix4x4.CreateScale(Scale) *
                Matrix4x4.CreateFromQuaternion(Rotation);

            WorldTransform = Matrix4x4.Transpose(world.HasValue ? world.Value * LocalTransform : LocalTransform);
        }

        public void Init()
        {
            Scale = Vector3.One;
            Rotation = Quaternion.Identity;
            Position = Vector3.Zero;
            

            // This might not be needed since they wont be available to other systems in first loop
            LocalTransform = Matrix4x4.Identity;
            WorldTransform = Matrix4x4.Identity;
        }
    }
}
