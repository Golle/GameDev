using System;
using Titan.Core;
using Titan.Core.GameLoop;
using Titan.Core.Ioc;
using Titan.Core.Logging;
using Titan.Graphics;
using Titan.Windows;

namespace Titan
{
    public class Application<T> where T : class, new()
    {
        private readonly IContainer _container = Bootstrapper.CreateContainer()
                .AddRegistry<WindowsRegistry>()
                .AddRegistry<GraphicsRegistry>();

        private readonly IGraphicsHandler _graphicsHandler;
        private readonly ILogger _logger;
        private readonly IGameLoop _gameLoop;

        public Application()
        {
            _graphicsHandler = _container.CreateInstance<IGraphicsHandler>();
            _logger = _container.GetInstance<ILogger>();
            _gameLoop = _container.GetInstance<IGameLoop>();
        }

        public static void Start()
        {
            var application = new T() as Application<T> ?? throw new InvalidOperationException("Failed to Create Application class");
            application.Run();
        }

        private void Run()
        {
            _logger.Debug("Initialize Window");
            if (!_graphicsHandler.Initialize("A game title", 1024, 768))
            {
                throw new InvalidOperationException("Failed to create the window");
            }
            OnStart();


            _logger.Debug("Start main loop");
            _gameLoop.Run(_graphicsHandler.Update);
            
            _logger.Debug("Main loop ended");
            OnQuit();

            _logger.Debug("Dispose main loop");
            _graphicsHandler.Dispose();
            _logger.Debug("Ending application");
        }

        protected virtual void OnStart(){}
        protected virtual void OnQuit() { }
    }
}
