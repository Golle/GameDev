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
        private readonly IInputManager _inputManager;

        public GameEngine(IWindow window, IDevice device, IEventManager eventManager, ILogger logger, IInputManager inputManager)
        {
            _window = window;
            _device = device;
            _eventManager = eventManager;
            _logger = logger;
            _inputManager = inputManager;
        }

        public bool Execute()
        {
            // temporary for testing
            var mousePosition = _inputManager.Mouse.Position;
            _window.SetTitle($"x: {mousePosition.X} y: {mousePosition.Y}");


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
