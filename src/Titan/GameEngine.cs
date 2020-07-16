using Titan.Core.EventSystem;
using Titan.Core.Logging;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan
{
    internal class GameEngine
    {
        private readonly IWindow _window;
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;
        private readonly IInputManager _inputManager;
        public GameEngine(IWindow window, IEventManager eventManager, ILogger logger, IInputManager inputManager)
        {
            _window = window;
            _eventManager = eventManager;
            _logger = logger;
            _inputManager = inputManager;
        }

        public bool Execute()
        {
            _inputManager.Update(); // update all input (move current state to previous state
            if (!_window.Update()) // Read new inputs
            {
                _logger.Debug("Window kill commands received, exiting Engine.");
                return false;
            }
            _eventManager.Update();

            // Update physics?

            // Execute queued sounds



            // Draw 3D World
            // Draw the UI/Hud
            //_renderer.Push(_model);
            //_renderer.Flush();


            //_camera.RotateX((-_inputManager.Mouse.Position.Y/(float)_window.Width) * 2f);
            //_camera.RotateZ((_inputManager.Mouse.Position.X/ (float)_window.Height) * 2f);


            //_renderer.BeginScene(_camera);

            //_renderer.EndScene();

            //_spriteBatchRenderer.Flush();
            //_spriteBatchRenderer.Render();

            return true;
        }
    }
}
