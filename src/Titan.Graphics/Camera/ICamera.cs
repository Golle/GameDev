using System.Numerics;

namespace Titan.Graphics.Camera
{
    public interface ICamera
    {
        ref readonly Matrix4x4 ViewProjection { get; }
        ref readonly Matrix4x4 View { get; }
        ref readonly Matrix4x4 Projection { get; }


        void RotateZ(float radians);
        void RotateX(float radians);
    }
}
