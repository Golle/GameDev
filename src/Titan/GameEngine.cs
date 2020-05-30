using System;
using System.Diagnostics;
using System.Numerics;
using Titan.Core.EventSystem;
using Titan.Core.Logging;
using Titan.D3D11;
using Titan.Graphics.Camera;
using Titan.Graphics.Renderer;
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
        private readonly ICameraFactory _cameraFactory;
        private readonly IRenderer _renderer;

        private ICamera _camera;
        private RendereableModel _model;
        public GameEngine(IWindow window, IEventManager eventManager, ILogger logger, IInputManager inputManager, ICameraFactory cameraFactory, IRenderer renderer)
        {
            _window = window;
            _eventManager = eventManager;
            _logger = logger;
            _inputManager = inputManager;
            _cameraFactory = cameraFactory;
            _renderer = renderer;

            _camera = _cameraFactory.CreatePerspectiveCamera();
        }

        public bool Execute()
        {
            var mousePosition = _inputManager.Mouse.Position;


            //_window.SetTitle($"x: {mousePosition.X} y: {mousePosition.Y}");
            

            if (!_window.Update())
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

            
            return true;
        }
    }
}
