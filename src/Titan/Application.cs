using System;
using System.Net.WebSockets;
using System.Numerics;
using Titan.Components;
using Titan.Configuration;
using Titan.Core;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop;
using Titan.Core.GameLoop.Events;
using Titan.Core.Ioc;
using Titan.Core.Logging;
using Titan.Core.Math;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.Runners;
using Titan.ECS.Systems;
using Titan.Graphics;
using Titan.Graphics.Models;
using Titan.Graphics.Textures;
using Titan.Scenes;
using Titan.Systems;
using Titan.Systems.Debugging;
using Titan.Systems.Rendering;
using Titan.Windows;
using Titan.Windows.Window;

namespace Titan
{
    public interface IWorldBuilder
    {
        string SceneDescriptor();
        IWorld CreateWorld(IWorld world);
        SystemsRunnerBuilder ConfigureSystems(SystemsRunnerBuilder builder);
    }

    public class Application<T> where T : class, new()
    {
        private readonly IContainer _container = Bootstrapper.CreateContainer()
                .AddRegistry<WindowsRegistry>()
                .AddRegistry<GraphicsRegistry>()
                .AddRegistry<EngineRegistry>()
                .Register<GameEngine>()
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

            _logger.Debug("Initialize Sandbox");
            OnInitialize(_container);

            var descriptor = GetInstance<ISceneParser>()
                .Parse(@"F:\Git\GameDev\src\Titan.Game\Scenes\scene01.json");

            var (world, systemsRummer) = CreateWorld(descriptor.Configuration);

            // Set the resource managers as a handler for the world
            foreach (var resourceManager in _container.GetAll<IResourceManager>())
            {
                resourceManager.Manage(world);
            }

            // Call the virual method to build the world
            BuildWorld(world);

            _container.GetInstance<IEventManager>()
                .Subscribe((in UpdateEvent @event) =>
                {
                    display.Device.BeginRender();
                    systemsRummer.Update(@event.ElapsedTime);
                    display.Device.EndRender();
                });

            var engine = _container.GetInstance<GameEngine>();
            
            OnStart();
            _logger.Debug("Start main loop");
            
            //world.WriteToStream();

            GetInstance<IGameLoop>().Run(engine.Execute);

            //_world.Destroy();
            _logger.Debug("Main loop ended");
            OnQuit();
            world.Dispose();
            _logger.Debug("Ending application");

        }


        protected virtual SystemsRunnerBuilder ConfigureSystems(SystemsRunnerBuilder builder) => builder;
        protected virtual IWorld BuildWorld(IWorld world) => world;

        private (IWorld world, ISystemsRunner systemsRummer) CreateWorld(SceneConfiguration configuration)
        {

            var builder = new WorldBuilder(maxEntities: configuration.MaxEntities, defaultComponentPoolSize: configuration.ComponentPoolDefaultSize);
            foreach (var component in configuration.Components)
            {
                var type = Type.GetType(component.Name, throwOnError: true);
                builder.WithComponent(type, component.Size);
            }

            foreach (var resource in configuration.Resources)
            {
                var identifierType = Type.GetType(resource.Identifier, throwOnError: true);
                var resourceType = Type.GetType(resource.Type, throwOnError: true);
                builder.WithComponent(typeof(Resource<,>).MakeGenericType(identifierType, resourceType));
            }

            var world = builder.Build();

            var systemsRunner = ConfigureSystems(new SystemsRunnerBuilder(world, _container.CreateChildContainer()))
                .WithSystem<MovementSystem2D>()
                .WithSystem<MovementSystem3D>()
                .WithSystem<Transform2DEntitySystem>()
                .WithSystem<Transform3DEntitySystem>()

                .WithSystem<Camera3DSystem>()
                .WithSystem<LightSystem>()
                .WithSystem<Model3DRenderSystem>()
                .WithSystem<BoundingBoxSystem>()
                
                .WithSystem<SpriteRenderSystem>()
                
                
                .WithSystem<UIRenderSystem>()
                .Build();

            return (world, systemsRunner);
        }

        protected virtual void OnInitialize(IContainer container) { }
        protected virtual void OnStart() { }
        
        protected virtual void OnQuit() { }
        protected virtual void RegisterServices(IContainer container) { }

        //protected virtual void RegisterComponents(Something something) { } TODO: add implementation for this
        private TType GetInstance<TType>() => _container.GetInstance<TType>();
    }

    
}
