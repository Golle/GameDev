using System;
using Titan.Configuration;
using Titan.Core;
using Titan.Core.GameLoop;
using Titan.Core.Ioc;
using Titan.Core.Logging;
using Titan.ECS;
using Titan.ECS.World;
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
                .AddRegistry<ECSGlobalRegistry>()
                .Register<GameEngine>()
                .Register<IEngineConfigurationHandler, EngineConfigurationHandler>()
            ;

        private readonly ILogger _logger;
        
        private IWorld _world;

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
            
            _logger.Debug("Initialize Window and D3D11Device");

            using var display = GetInstance<IDisplayFactory>()
                .Create("Donkey box", 1024, 768);

            _logger.Debug("Register D3D11Device and Win32Window as singletons");
            _container
                .RegisterSingleton(display.Device)
                .RegisterSingleton(display.Window);


            _world = CreateWorld();
            var entity = _world.CreateEntity();
            entity.AddComponent<TestComponent2>();
            entity.AddComponent<TestComponent1>();


            var engine = _container.GetInstance<GameEngine>();
            
            OnStart();

            _logger.Debug("Start main loop");
            GetInstance<IGameLoop>()
                .Run(engine.Execute);

            entity.Destroy();

            _world.Destroy();
            _logger.Debug("Main loop ended");
            OnQuit();
            _logger.Debug("Ending application");
        }

        private IWorld CreateWorld()
        {
            var configuration = new WorldConfigurationBuilder("Donkey")
                .WithContainer(_container.CreateChildContainer()) // Add a child container for this world (might be a better way to do this)
                .WithSystem<TestSystem>()
                .WithComponent<TestComponent1>(1000)
                .WithComponent<TestComponent2>(2000)
                .Build()
                ;

            return _container
                .GetInstance<IWorldCreator>()
                .CreateWorld(configuration);
        }

        protected virtual void OnStart() { }
        protected virtual void OnQuit() { }
        protected virtual void RegisterServices(IContainer container) { }

        //protected virtual void RegisterComponents(Something something) { } TODO: add implementation for this
        protected TType GetInstance<TType>() => _container.GetInstance<TType>();
    }
}
