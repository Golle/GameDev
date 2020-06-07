using System;
using System.Diagnostics;
using System.Numerics;
using Titan.Components;
using Titan.Configuration;
using Titan.Core;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop;
using Titan.Core.GameLoop.Events;
using Titan.Core.Ioc;
using Titan.Core.Logging;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.World;
using Titan.Graphics;
using Titan.Resources;
using Titan.Systems;
using Titan.Systems.Rendering;
using Titan.Windows;
using Titan.Windows.Window;

namespace Titan
{
    public class Application<T> where T : class, new()
    {
        private readonly IContainer _container = Bootstrapper.CreateContainer()
                .AddRegistry<WindowsRegistry>()
                .AddRegistry<GraphicsRegistry>()
                .AddRegistry<ECSGlobalRegistry>()
                .AddRegistry<EngineRegistry>()
                .Register<GameEngine>()
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

            _logger.Debug("Initialize Sandbox");
            OnInitialize(_container);
            
            _logger.Debug("Initialize Window and D3D11Device");

            using var display = GetInstance<IDisplayFactory>()
                .Create("Donkey box", 1920, 1080);

            _logger.Debug("Register D3D11Device and Win32Window as singletons");
            _container
                .RegisterSingleton(display.Device)
                .RegisterSingleton(display.Window);
            
            using var textureManager = GetInstance<ITextureManager>();
            _world = CreateWorld();

            var random = new Random();
            //for (var i = 0; i < 10900; ++i)


                //var entity1 = _world.CreateEntity();
                //entity1.AddComponent(new TransformRect { Position = new Vector3(600, 200, 0), Size = new Size(100) });
                //entity1.AddComponent(new Sprite { TextureCoordinates = TextureCoordinates.Default, Texture = textureManager.GetTexture(@"F:\Git\GameDev\resources\link.png"), Color = new Color(1, 1, 1) });


                //var entity2 = _world.CreateEntity();
                //entity2.AddComponent(new TransformRect { Position = new Vector3(100, 200, 0), Size = new Size(200) });
                //entity2.AddComponent(new Sprite { TextureCoordinates = TextureCoordinates.Default, Texture = textureManager.GetTexture(@"F:\Git\GameDev\resources\tree01.png"), Color = new Color(1, 1, 1) });
                
                //entity1.AddChild(entity2);

                var simpleEntity = _world.CreateEntity();
                _world.AddComponent(simpleEntity, new Transform2D { Position = new Vector2(1920 / 2f, 1080 / 2f) });

                var parentEntity = _world.CreateEntity();
                _world.AddComponent(parentEntity, new Transform2D{Position = new Vector2(1920 / 2f, 1080 / 2f) });

                for (var i = 0; i < 1000; ++i)
                {
                    var entity3 = _world.CreateEntity();
                    _world.AddComponent(entity3, new Transform2D { Position = Vector2.Zero, Scale = Vector2.One });
                    _world.AddComponent(entity3, new Velocity { Value = new Vector3(random.Next(-5000, 5000) / 100f, random.Next(-5000, 5000) / 100f, 0) });
                    _world.AddComponent(entity3, new Sprite { TextureCoordinates = TextureCoordinates.Default, Texture = textureManager.GetTexture(@"F:\Git\GameDev\resources\link.png"), Color = new Color(1, 1, 1) });
                    _world.SetParent(entity3, parentEntity);
                    //parentEntity.AddChild(entity3);
                }
                
                
            var engine = _container.GetInstance<GameEngine>();
            
            OnStart();
            var count = 300;
            _logger.Debug("Start main loop");
            GetInstance<IGameLoop>()
                .Run(() =>
                {
                    var execute = engine.Execute();
                    if (count-- == 0)
                    {
                        //simpleEntity.Destroy();
                        //var s = Stopwatch.StartNew();
                        //parentEntity?.Destroy(); parentEntity = null;
                        //s.Stop();

                        //_logger.Debug("{0} ms to destroy the entity", s.Elapsed.TotalMilliseconds);
                        
                    }
                    return execute;
                });


            _world.Destroy();
            _logger.Debug("Main loop ended");
            OnQuit();
            _logger.Debug("Ending application");
        }

        private IWorld CreateWorld()
        {
            const int componentSize = 40000;

            var configuration = new WorldConfigurationBuilder("Donkey", maxNumberOfEntities: 10_000)
                .WithContainer(_container.CreateChildContainer()) // Add a child container for this world (might be a better way to do this)
                //.WithSystem<MovementSystem3D>()
                .WithSystem<MovementSystem2D>()

                //.WithSystem<D3D11ModelLoaderSystem>()
                //.WithSystem<D3D11ShaderLoaderSystem>()
                //.WithSystem<D3D11Camera3DSystem>()
                //.WithSystem<D3D11Render3DSystem>()
                .WithSystem<Transform2DSystem>()
                .WithSystem<SpriteRenderSystem>()
                .WithSystem<UIRenderSystem>()
                //.WithComponent<Transform3D>(componentSize)
                .WithComponent<Transform2D>(componentSize)
                .WithComponent<Velocity>(componentSize)
                //.WithComponent<Mesh>(componentSize)
                //.WithComponent<Shader>(componentSize)
                .WithComponent<TransformRect>(componentSize)
                .WithComponent<Sprite>(componentSize)

                //.WithComponent<D3D11Model>(componentSize)
                //.WithComponent<D3D11Shader>(componentSize)
                .WithComponent<Camera>(10)

                .Build()
                ;

            return _container
                .GetInstance<IWorldCreator>()
                .CreateWorld(configuration);
        }

        protected virtual void OnInitialize(IContainer container) { }
        protected virtual void OnStart() { }
        
        protected virtual void OnQuit() { }
        protected virtual void RegisterServices(IContainer container) { }

        //protected virtual void RegisterComponents(Something something) { } TODO: add implementation for this
        private TType GetInstance<TType>() => _container.GetInstance<TType>();
    }
}
