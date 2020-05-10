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

        private readonly ILogger _logger;
        private readonly IGameLoop _gameLoop;
        private readonly IDisplayFactory _displayFactory;

        public Application()
        {
            //_graphicsHandler = _container.CreateInstance<IGraphicsHandler>();
            _displayFactory = _container.CreateInstance<IDisplayFactory>();
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

            using var display = _displayFactory.Create("Donkey box", 1024, 768);

            OnStart();
            _logger.Debug("Start main loop");
            
            _gameLoop.Run(display.Update);
            
            _logger.Debug("Main loop ended");
            OnQuit();
            _logger.Debug("Ending application");
        }

        protected virtual void OnStart(){}
        protected virtual void OnQuit() { }
    }
}
