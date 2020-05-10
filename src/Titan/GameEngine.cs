using Titan.Core.EventSystem;
using Titan.Core.Logging;
using Titan.Graphics.Stuff;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan
{
    internal class GameEngine
    {
        private readonly IWindow _window;
        private readonly IDevice _device;
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;

        public GameEngine(IWindow window, IDevice device, IEventManager eventManager, ILogger logger)
        {
            _window = window;
            _device = device;
            _eventManager = eventManager;
            _logger = logger;
        }

        public bool Execute()
        {
            if (!_window.Update())
            {
                _logger.Debug("Window kill commands received, exiting Engine.");
                return false;
            }
            _eventManager.Update();

            // Update physics?

            // Execute queued sounds

            _device.BeginRender();

            // Draw 3D World
            // Draw the UI/Hud

            _device.EndRender();

            return true;
        }
    }
}
