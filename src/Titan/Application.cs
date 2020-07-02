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
using Titan.Core.Math;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.Runners;
using Titan.ECS.Systems;
using Titan.Graphics;
using Titan.Graphics.Models;
using Titan.Graphics.Textures;
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

            _logger.Debug("Initialize Sandbox");
            OnInitialize(_container);
            
            _logger.Debug("Initialize Window and D3D11Device");

            using var display = GetInstance<IDisplayFactory>()
                .Create("Donkey box", 1920, 1080);

            _logger.Debug("Register D3D11Device and Win32Window as singletons");
            _container
                .RegisterSingleton(display.Device)
                .RegisterSingleton(display.Window);



            var (world, systemsRummer) = CreateWorld();

            // Set the resource managers as a handler for the world
            foreach (var resourceManager in _container.GetAll<IResourceManager>())
            {
                resourceManager.Manage(world);
            }

            _container.GetInstance<IEventManager>()
                .Subscribe((in UpdateEvent @event) =>
                {
                    display.Device.BeginRender();
                    systemsRummer.Update(@event.ElapsedTime);
                    display.Device.EndRender();
                });


            //var parentEntity = world.CreateEntity();
            //parentEntity.AddComponent(new Transform2D { Position = new Vector2(1920 / 2f, 1080 / 2f) }); 
            
            var random = new Random();
        
            var entity1 = world.CreateEntity();
            entity1.AddComponent(new TransformRect { Position = new Vector3(600, 200, 0), Size = new Size(100) });
            entity1.AddComponent(new Sprite { TextureCoordinates = TextureCoordinates.Default, Color = new Color(1, 1, 1) });
            entity1.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\link.png"));
            
            //entity1.AddComponent(new Resource<(string, VertexLayout), (IVertexShader, IInputLayout)>(("Shaders/VertexShader.cso", ColoredVertex.VertexLayout)));
            //entity1.AddComponent(new Resource<string, IPixelShader>("Shaders/PixelShader.cso"));

            //for (var j = 0; j < 2; ++j)
            //{
            //    var entity3 = world.CreateEntity();
            //    entity3.AddComponent(new Transform2D { Position = Vector2.Zero, Scale = Vector2.One });
            //    entity3.AddComponent(new Velocity { Value = new Vector3(random.Next(-5000, 5000) / 100f, random.Next(-5000, 5000) / 100f, 0) });
            //    entity3.AddComponent(new Sprite { TextureCoordinates = TextureCoordinates.Default, Color = new Color(1, 1, 1) });
            //    entity3.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\link.png"));
            //    parentEntity.Attach(entity3);
            //}

            //{
            //    var entity = world.CreateEntity();
            //    entity.AddComponent(new Transform3D { Position =new Vector3(0, -1, 0), Scale = Vector3.One });
            //    //entity.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\thor hammer.obj"));
            //    entity.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\untitled3.obj"));
            //    //entity.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\untitled2.obj"));
            //    //entity.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\cottage_obj.obj"));
            //    entity.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\cottage_diffuse.png"));
            //}
            
            //{
            //    var entity = world.CreateEntity();
            //    entity.AddComponent(new Transform3D { Position =new Vector3(0, 1, 0), Scale = new Vector3(0.3f)});
            //    entity.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\untitled3.obj"));
            //    entity.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\cottage_diffuse.png"));
            //}
            
            //{
            //    var entity = world.CreateEntity();
            //    entity.AddComponent(new Transform3D { Position =new Vector3(0, 1, 0), Scale = new Vector3(2f) });
            //    entity.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\untitled2.obj"));
            //    entity.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\cottage_diffuse.png"));
            //    entity.AddComponent(new Velocity{Value = new Vector3(0, -0.4f, 0)});
            //}

            {
                var sphere = world.CreateEntity();
                sphere.AddComponent(new Transform3D { Position = new Vector3(-3f, 0, 2f), Scale = new Vector3(1f) });
                sphere.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere1.obj"));
                sphere.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\red.png"));
            }

            {
                var sphere = world.CreateEntity();
                sphere.AddComponent(new Transform3D { Position = new Vector3(2f, 0, 3f), Scale = new Vector3(1f) });
                sphere.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                sphere.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\red.png"));
            }

            {
                var sphere = world.CreateEntity();
                sphere.AddComponent(new Transform3D { Position = new Vector3(0, 3f, 2f), Scale = new Vector3(1f) });
                sphere.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\cube.obj"));
                sphere.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\red.png"));
            } 
            
            {
                var sphere = world.CreateEntity();
                sphere.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 7f), Scale = new Vector3(10f, 10f, 1f) });
                sphere.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\cube.obj"));
                sphere.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\blue.png"));
            }

            {
                var light = world.CreateEntity();
                light.AddComponent(new Light{Color = new Color(1, 1f, 1f)});
                light.AddComponent(new Transform3D { Position = new Vector3(1f, 1f, 0), Scale = new Vector3(0.1f)});
                light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\blue.png"));
                light.AddComponent(new Velocity{Value = new Vector3{X = -4.5f, Y = 6f}});
            }
            
            {
                var light = world.CreateEntity();
                light.AddComponent(new Light{Color = new Color(1, 1f, 1f)});
                light.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0), Scale = new Vector3(0.1f)});
                light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\blue.png"));
                light.AddComponent(new Velocity{Value = new Vector3{X = 2.5f, Y = 3f}});
            }
            
            {
                var light = world.CreateEntity();
                light.AddComponent(new Light{Color = new Color(1, 1f, 1f)});
                light.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0), Scale = new Vector3(0.1f)});
                light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\blue.png"));
                light.AddComponent(new Velocity{Value = new Vector3{X = 3.5f, Y = -8f}});
            }
            var engine = _container.GetInstance<GameEngine>();
            
            OnStart();
            var count = 300;
            _logger.Debug("Start main loop");
            
            GetInstance<IGameLoop>()
                .Run(() =>
                {
                    var execute = engine.Execute();

                    //if (count-- == 0)
                    //{
                    //    parentEntity.RemoveComponent<Transform2D>();
                    //}

                    //if (count == -300)
                    //{
                    //    parentEntity.AddComponent(new Transform2D {Position = new Vector2(1920 / 2f, 1080 / 2f)});

                    //}

                    //if (count == -300)
                    //{
                    //    parentEntity.Destroy();
                    //    entity1.Destroy();
                    //}
                

                    return execute;
                });


            //_world.Destroy();
            _logger.Debug("Main loop ended");
            OnQuit();
            world.Dispose();
            _logger.Debug("Ending application");

        }


        private (IWorld world, ISystemsRunner systemsRummer) CreateWorld()
        {
            var world = new WorldBuilder(maxEntities: 100000, defaultComponentPoolSize: 100000)
                .WithComponent<Transform2D>()
                .WithComponent<Transform3D>()
                .WithComponent<TransformRect>()
                .WithComponent<Light>()
                .WithComponent<Velocity>()
                .WithComponent<Sprite>()
                .WithComponent<Camera>(10)
                .WithComponent<Model3D>()
                .WithComponent<Texture2D>()

                // TODO: Add a better way to handle resources
                .WithComponent<Resource<string, ITexture2D>>()
                .WithComponent<Resource<string, IMesh>>()
                
                
                //.WithComponent<VertexShader>()
                //.WithComponent<PixelShader>()
                .Build();

            var systemsRummer = new SystemsRunnerBuilder(world, _container.CreateChildContainer())
                .WithSystem<MovementSystem2D>()
                .WithSystem<Transform2DEntitySystem>()
                .WithSystem<Model3DRenderSystem>()
                .WithSystem<Transform3DEntitySystem>()
                .WithSystem<MovementSystem3D>()
                .WithSystem<LightSystem>()
                .WithSystem<SpriteRenderSystem>()
                .WithSystem<UIRenderSystem>()
                .Build();
                //.WithSystem<MovementSystem2D>();

            //var configuration = new WorldConfigurationBuilder("Donkey", maxNumberOfEntities: 10_000)
            //    .WithContainer(_container.CreateChildContainer()) // Add a child container for this world (might be a better way to do this)
            //    //.WithSystem<MovementSystem3D>()
            //    .WithSystem<MovementSystem2D>()

            //    //.WithSystem<D3D11ModelLoaderSystem>()
            //    //.WithSystem<D3D11ShaderLoaderSystem>()
            //    //.WithSystem<D3D11Camera3DSystem>()
            //    //.WithSystem<D3D11Render3DSystem>()
            //    .WithSystem<Transform2DSystem>()
            //    .WithSystem<SpriteRenderSystem>()
            //    .WithSystem<UIRenderSystem>()
            //    //.WithComponent<Transform3D>(componentSize)
            //    .WithComponent<Transform2D>(componentSize)
            //    .WithComponent<Velocity>(componentSize)
            //    //.WithComponent<Mesh>(componentSize)
            //    //.WithComponent<Shader>(componentSize)
            //    .WithComponent<TransformRect>(componentSize)
            //    .WithComponent<Sprite>(componentSize)

            //    //.WithComponent<D3D11Model>(componentSize)
            //    //.WithComponent<D3D11Shader>(componentSize)
            //    .WithComponent<Camera>(10)

            //    .Build()
            //    ;

            return (world, systemsRummer);
        }

        protected virtual void OnInitialize(IContainer container) { }
        protected virtual void OnStart() { }
        
        protected virtual void OnQuit() { }
        protected virtual void RegisterServices(IContainer container) { }

        //protected virtual void RegisterComponents(Something something) { } TODO: add implementation for this
        private TType GetInstance<TType>() => _container.GetInstance<TType>();
    }
}
