using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            return new OrthographicCamera(_eventManager, -aspectRatio * 0.25f, aspectRatio*0.25f, -0.25f, 0.25f);

        }

        public ICamera CreatePerspectiveCamera()
        {
            return new PerspectiveCamera(1f, _window.Height / (float)_window.Width, 0.5f, 10f);
        }
    }
}
