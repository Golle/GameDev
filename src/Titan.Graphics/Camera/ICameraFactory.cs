using Titan.Core.EventSystem;
using Titan.Windows.Window;

namespace Titan.Graphics.Camera
{
    public interface ICameraFactory
    {
        ICamera CreateOrhographicCamera();
        ICamera CreatePerspectiveCamera();
    }

    class CameraFactory : ICameraFactory
    {
        private readonly IEventManager _eventManager;
        private readonly IWindow _window;

        public CameraFactory(IEventManager eventManager, IWindow window)
        {
            _eventManager = eventManager;
            _window = window;
        }
        public ICamera CreateOrhographicCamera()
        {

            var aspectRatio = _window.Width / (float)_window.Height;

            var windowWidth = _window.Width/2f;
            var windowHeight = _window.Height/2f;

            //return new OrthographicCamera(_eventManager, -windowWidth, windowWidth, -windowHeight, windowHeight);
            return new OrthographicCamera(_eventManager, 0, _window.Width, 0, _window.Height);

        }

        public ICamera CreatePerspectiveCamera()
        {
            return new PerspectiveCamera(1f, _window.Height / (float)_window.Width, 0.5f, 1000f);
        }
    }
}
