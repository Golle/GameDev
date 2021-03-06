using System.Numerics;
using Titan.Core.Math;

namespace Titan.Graphics.Camera
{
    internal class PerspectiveCamera : ICamera
    {
        private Matrix4x4 _projection;
        private Matrix4x4 _view;
        private Matrix4x4 _viewProjection;
        private float _rotationZ = 0f;
        private float _rotationX = 0f;
        private float _rotationY = 0f;
        public ref readonly Matrix4x4 ViewProjection => ref _viewProjection;
        public ref readonly Matrix4x4 View => ref _view;
        public ref readonly Matrix4x4 Projection => ref _projection;


        public PerspectiveCamera(float width, float height, float nearPlane, float farPlane)
        {
            _projection = MatrixExtensions.CreatePerspectiveLH(width, height, nearPlane, farPlane);
            _view = Matrix4x4.Identity;

            CalculateViewProjectionMatrix();
        }

        public void RotateZ(float radians)
        {
            _rotationZ = radians;
            CalculateViewProjectionMatrix();
        }

        public void RotateX(float radians)
        {
            _rotationX = radians;
            CalculateViewProjectionMatrix();
        }

        public void RotateY(float radians)
        {
            _rotationY = radians;
            CalculateViewProjectionMatrix();
        }


        private Vector3 _position;
        public void Move(in Vector3 vector3)
        {
            _position += vector3;
            CalculateViewProjectionMatrix();
        }


        private void CalculateViewProjectionMatrix()
        {
            _viewProjection = Matrix4x4.Transpose(
                Matrix4x4.CreateTranslation(_position) *
                Matrix4x4.CreateRotationZ(_rotationZ) *
                Matrix4x4.CreateRotationX(_rotationX) *
                Matrix4x4.CreateRotationY(_rotationY) *
                
                _projection
            );
        }
    }
}
