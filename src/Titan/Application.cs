using System;
using System.Threading;
using Titan.Core;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop;
using Titan.Core.GameLoop.Events;
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
        private long _fixedUpdate;
        private long _update;

        public Application()
        {
            _graphicsHandler = _container.CreateInstance<IGraphicsHandler>();
            _logger = _container.GetInstance<ILogger>();
            _gameLoop = _container.GetInstance<IGameLoop>();
            _container.GetInstance<IEventManager>()
                .Subscribe<FixedUpdateEvent>(FixedUpdate);
            _container.GetInstance<IEventManager>()
                .Subscribe<UpdateEvent>(Update);
        }

        private float _elapsedTime = 0f;
        private void Update(in UpdateEvent @event)
        {
            _update++;
            _elapsedTime += @event.ElapsedTime;
            if (_elapsedTime >= 5.0f)
            {
                Console.WriteLine($"Update: {_update}. Fixed Update: {_fixedUpdate}");
                _elapsedTime = 0f;
                _fixedUpdate = 0;
                _update = 0;
            }
        }

        private void FixedUpdate(in FixedUpdateEvent @event)
        {
            _fixedUpdate++;
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
