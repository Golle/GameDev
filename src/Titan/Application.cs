using System;
using System.Numerics;
using Titan.Components;
using Titan.Configuration;
using Titan.Core;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop;
using Titan.Core.GameLoop.Events;
using Titan.Core.Ioc;
using Titan.Core.Logging;
using Titan.ECS;
using Titan.ECS.World;
using Titan.Graphics;
using Titan.Systems;
using Titan.Windows;
using Titan.Windows.Window;

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

        private float _fps;
        private void Run()
        {
            _container.GetInstance<IEventManager>()
                .Subscribe<UpdateEvent>((in UpdateEvent @event) =>
                {
                    _fps = 1f / @event.ElapsedTime;
                });
            _container.GetInstance<IEventManager>()
                .Subscribe<FixedUpdateEvent>((in FixedUpdateEvent @event) =>
                {
                    _container.GetInstance<IWindow>().SetTitle($"FPS: {_fps}");
                });


            _logger.Debug("Initialize EngineConfiguration");
            _container
                .GetInstance<IEngineConfigurationHandler>()
                .Initialize();

            RegisterServices(_container);
            
            _logger.Debug("Initialize Window and D3D11Device");

            using var display = GetInstance<IDisplayFactory>()
                .Create("Donkey box", 1920, 1080);

            _logger.Debug("Register D3D11Device and Win32Window as singletons");
            _container
                .RegisterSingleton(display.Device)
                .RegisterSingleton(display.Window);


            _world = CreateWorld();


            var random = new Random();
            for (var i = 0; i < 1000; ++i)
            {
                var entity1 = _world.CreateEntity();
                entity1.AddComponent(new Velocity { Value = new Vector3(random.Next(-3000, 3000) / 1000f, random.Next(-3000, 3000) / 1000f, random.Next(-3000, 3000) / 1000f) });
                entity1.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0f) });
                entity1.AddComponent<Mesh>();
                entity1.AddComponent<Material>();
            }

            var engine = _container.GetInstance<GameEngine>();
            
            OnStart();

            _logger.Debug("Start main loop");
            GetInstance<IGameLoop>()
                .Run(engine.Execute);


            _world.Destroy();
            _logger.Debug("Main loop ended");
            OnQuit();
            _logger.Debug("Ending application");
        }

        private IWorld CreateWorld()
        {
            var configuration = new WorldConfigurationBuilder("Donkey")
                .WithContainer(_container.CreateChildContainer()) // Add a child container for this world (might be a better way to do this)
                .WithSystem<MovementSystem>()
                .WithSystem<Render3DSystem>()
                .WithComponent<Transform2D>(1000)
                .WithComponent<Transform3D>(1000)
                .WithComponent<Velocity>(1000)
                .WithComponent<Mesh>(1000)
                .WithComponent<Material>(1000)

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
