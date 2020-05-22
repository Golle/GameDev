using System;
using Titan.Configuration;
using Titan.Core;
using Titan.Core.GameLoop;
using Titan.Core.Ioc;
using Titan.Core.Logging;
using Titan.EntityComponentSystem;
using Titan.Graphics;
using Titan.Systems;
using Titan.Windows;

namespace Titan
{
    public class Application<T> where T : class, new()
    {
        private readonly IContainer _container = Bootstrapper.CreateContainer()
                .AddRegistry<WindowsRegistry>()
                .AddRegistry<GraphicsRegistry>()
                .AddRegistry<SystemsRegistry>()
                .AddRegistry<EntityComponenSystemRegistry>()
                .Register<GameEngine>()
                .Register<IEngineConfigurationHandler, EngineConfigurationHandler>()
            ;

        private readonly ILogger _logger;

        public Application()
        {
            _logger = _container.GetInstance<ILogger>();
        }

        public static void Start()
        {
            var application = new T() as Application<T> ?? throw new InvalidOperationException("Failed to Create Application class");
            application.Run();
        }

        private void Run()
        {

            _logger.Debug("Initialize EngineConfiguration");
            _container
                .GetInstance<IEngineConfigurationHandler>()
                .Initialize();

            RegisterServices(_container);
            //RegisterComponentPools(_container.GetInstance<IComponentManager>());
            
            _logger.Debug("Initialize Window and D3D11Device");

            using var display = GetInstance<IDisplayFactory>()
                .Create("Donkey box", 1024, 768);

            _logger.Debug("Register D3D11Device and Win32Window as singletons");
            _container
                .RegisterSingleton(display.Device)
                .RegisterSingleton(display.Window);


            var engine = _container.GetInstance<GameEngine>();
            
            OnStart();

            _logger.Debug("Start main loop");
            GetInstance<IGameLoop>()
                .Run(engine.Execute);

            _logger.Debug("Main loop ended");
            OnQuit();
            _logger.Debug("Ending application");
        }

        protected virtual void OnStart() { }
        protected virtual void OnQuit() { }
        protected virtual void RegisterServices(IContainer container) { }
        //protected virtual void RegisterComponentPools(IComponentManager componentManager){}
        protected TType GetInstance<TType>() => _container.GetInstance<TType>();
    }
}
