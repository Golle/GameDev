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
            return new OrthographicCamera(_eventManager, _window.Width, _window.Height);

        }
    }
}
