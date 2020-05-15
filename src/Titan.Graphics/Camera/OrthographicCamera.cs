using System;
using Titan.Core.EventSystem;
using Titan.Windows.Window.Events;

namespace Titan.Graphics.Camera
{
    internal class OrthographicCamera : ICamera
    {
        private int _width;
        private int _height;

        public OrthographicCamera(IEventManager eventManager, int width, int height)
        {
            _width = width;
            _height = height;
            eventManager.Subscribe<WindowResizeEvent>(OnWindowResize);
        }

        private void OnWindowResize(in WindowResizeEvent @event)
        {
            _height = @event.Height;
            _width= @event.Width;
            Console.WriteLine($"Width: {_width}, Height: {_height}");
        }
    }
}
