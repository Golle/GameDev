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
            Setup();
        }

        private void Setup()
        {
            _camera = _cameraFactory.CreatePerspectiveCamera();
            var vertices = new []
            {
                new Vertex {Color = new Color(1f, 0, 0),  X = -1f, Y = -1f, Z = -1f},
                new Vertex {Color = new Color(1f, 1f, 0), X = 1f, Y = -1f, Z = -1f},
                new Vertex {Color = new Color(1f, 0, 1f), X = -1f, Y = 1f, Z = -1f},
                new Vertex {Color = new Color(0f, 1f, 0), X = 1f, Y = 1f, Z = -1f},
                new Vertex {Color = new Color(0f, 1f, 1f),X = -1f, Y = -1f, Z = 1f},
                new Vertex {Color = new Color(1f, 1f, 0), X = 1f, Y = -1f, Z = 1f},
                new Vertex {Color = new Color(1f, 1f, 1f),X = -1f, Y = 1f, Z = 1f},
                new Vertex {Color = new Color(1f, 0, 0),  X = 1f, Y = 1f, Z = 1f},
            };
            var indices = new short[]
            {
                0,2,1,  2,3,1,
                1,3,5,  3,7,5,
                2,6,3,  3,6,7,
                4,5,7,  4,7,6,
                0,4,2,  2,4,6,
                0,1,4,  1,5,4
            };
            _model = new RendereableModel(vertices, indices, Vector3.Zero);
            
        }

        public bool Execute()
        {
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


            
            // Draw 3D World
            // Draw the UI/Hud
            _renderer.Push(_model);
            _renderer.Flush();


            _camera.RotateX((-_inputManager.Mouse.Position.Y/(float)_window.Width) * 2f);
            _camera.RotateZ((_inputManager.Mouse.Position.X/ (float)_window.Height) * 2f);

            
            _renderer.BeginScene(_camera);

            

            _renderer.EndScene();

            return true;
        }
    }
}
