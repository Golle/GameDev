using System;
using System.IO;
using Titan.Components;
using Titan.Core;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop;
using Titan.Core.GameLoop.Events;
using Titan.Core.Ioc;
using Titan.Core.Logging;
using Titan.D3D11;
using Titan.D3D11.Compiler;
using Titan.ECS;
using Titan.ECS.Runners;
using Titan.ECS.Systems;
using Titan.Graphics;
using Titan.Graphics.RendererOld;
using Titan.Scenes;
using Titan.Sound;
using Titan.Systems;
using Titan.Systems.Debugging;
using Titan.Systems.Rendering;
using Titan.Systems.UI;
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
                .AddRegistry<SoundRegistry>()
                .AddRegistry<EngineRegistry>()
                .Register<GameEngine>()
            ;

        public static void Start()
        {
            var application = new T() as Application<T> ?? throw new InvalidOperationException("Failed to Create Application class");
            application.Run();
        }

        private float _fps;

        private void Run()
        {
            SetFpsCounter();

            using var startup = _container.CreateInstance<Startup>();
            
            RegisterServices(_container);
            
            startup.InitializeEngine(_container);


            var logger = _container.GetInstance<ILogger>();

            /*******Test compile shaders*******/
            TextCompileShaders();


            logger.Debug("Initialize Sandbox");
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
            var device = GetInstance<IDevice>();
            _container.GetInstance<IEventManager>()
                .Subscribe((in UpdateEvent @event) =>
                {
                    var context = device.ImmediateContext;
                    context.ClearRenderTarget(device.BackBuffer, new Color { B = 0.1f});
                    context.ClearDepthStencil(device.DepthStencil);

                    systemsRummer.Update(@event.ElapsedTime);

                    device.EndRender();
                });

            var engine = _container.GetInstance<GameEngine>();
            
            OnStart();
            logger.Debug("Start main loop");

            GetInstance<IGameLoop>()
                .Run(engine.Execute);

            logger.Debug("Main loop ended");

            OnQuit();
            
            world.Dispose();
            world = null; 
            systemsRummer = null;
            
            GetInstance<Renderer3Dv2>().Dispose();
            GetInstance<RendererDebug3Dv3>().Dispose();
            GetInstance<ISpriteBatchRenderer>().Dispose();
            logger.Debug("Ending application");
        }

        private void TextCompileShaders()
        {
            var d3dCompiler = GetInstance<ID3DCompiler>();
            foreach (var file in Directory.GetFiles(@"F:\Git\GameDev\resources\shaders\"))
            {
                using var blob =
                    d3dCompiler.CompileShaderFromFile(file, "main", file.Contains("VertexShader") ? "vs_5_0" : "ps_5_0");
            }
        }

        private void SetFpsCounter()
        {
            _container.GetInstance<IEventManager>()
                .Subscribe<UpdateEvent>((in UpdateEvent @event) => { _fps = 1f / @event.ElapsedTime; });
            _container.GetInstance<IEventManager>()
                .Subscribe<FixedUpdateEvent>((in FixedUpdateEvent @event) =>
                {
                    _container.GetInstance<IWindow>().SetTitle($"FPS: {_fps}");
                });
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
                //.WithSystem<UIButtonSystem>()

                .WithSystem<MovementSystem2D>()
                .WithSystem<MovementSystem3D>()
                .WithSystem<Transform2DEntitySystem>()
                .WithSystem<Transform3DEntitySystem>()
                .WithSystem<TransformRectSystem>()

                .WithSystem<Camera3DSystem>()
                //.WithSystem<LightSystem>()
                .WithSystem<Model3DRenderSystem>()
                

                .WithSystem<SpriteRenderSystem>()


                .WithSystem<UIRenderSystem>()
                .WithSystem<UITextRenderSystem>()
                .WithSystem<BoundingBoxSystem>()
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
