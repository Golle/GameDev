using System;
using System.Numerics;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop.Events;
using Titan.Core.Math;
using Titan.Windows.Window.Events;

namespace Titan.Graphics.Camera
{
    internal class OrthographicCamera : ICamera
    {
        private Matrix4x4 _projection;
        private Matrix4x4 _view;
        private Matrix4x4 _viewProjection;
        
        public ref readonly Matrix4x4 ViewProjection => ref _viewProjection;
        public ref readonly Matrix4x4 View => ref _view;
        public ref readonly Matrix4x4 Projection => ref _projection;
        public void RotateZ(float radians)
        {
        }

        public void RotateX(float radians)
        {
        }

        public OrthographicCamera(IEventManager eventManager, float width, float height, float bottom, float top)
        {
            _projection = Matrix4x4.CreateOrthographicOffCenter(width, height, bottom, top, -1f, 1f);
            _view = Matrix4x4.Identity;

            _viewProjection =
                Matrix4x4.Transpose(
                    _view *
                    _projection
                );

            eventManager.Subscribe<WindowResizeEvent>(OnWindowResize);
            eventManager.Subscribe<UpdateEvent>(OnUpdate);
        }

        private void OnUpdate(in UpdateEvent @event)
        {
            //_view = Matrix4x4.Transpose(
            //    Matrix4x4.CreateTranslation(0, 0f, 0) * 
            //    MatrixExtensions.CreatePerspectiveLH(_width, _height, 0.1f, 10f)
            //    );
        }

        private void OnWindowResize(in WindowResizeEvent @event)
        {
            var aspectRatio = @event.Width / (float)@event.Height;
            _projection = Matrix4x4.CreateOrthographicOffCenter(-aspectRatio * 0.25f, aspectRatio * 0.25f, -0.25f, 0.25f, -1f, 1f);
            _viewProjection = 
                Matrix4x4.Transpose(
                    _view *
                    _projection
                );
            ;
            ;
            Console.WriteLine($"Width: {@event.Width}, Height: {@event.Height}");
        }
    }
}
