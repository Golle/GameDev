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



            var descriptor = GetInstance<ISceneParser>()
                .Parse(@"F:\Git\GameDev\src\Titan.Game\Scenes\scene01.json");

            var (world, systemsRummer) = CreateWorld(descriptor.Configuration);

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
            entity1.AddComponent(new TransformRect { Position = new Vector3(1920/2f, 1080 / 2f, 0), Size = new Size(200, 100) });
            //entity1.AddComponent(new Sprite { TextureCoordinates = new TextureCoordinates(new Vector2(0f, 1f), new Vector2(1f/1f, 1f - 1f/32f)), Color = new Color(1, 1, 1) });
            entity1.AddComponent(new Sprite { TextureCoordinates = new TextureCoordinates { BottomRight = new Vector2(1f/8f, 1), TopLeft = new Vector2(0, 1f-1f/16f) }, Color = new Color(1, 1, 1) });
            entity1.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\ui_spritesheet.png"));

            //entity1.AddComponent(new Resource<(string, VertexLayout), (IVertexShader, IInputLayout)>(("Shaders/VertexShader.cso", ColoredVertex.VertexLayout)));
            //entity1.AddComponent(new Resource<string, IPixelShader>("Shaders/PixelShader.cso"));

            //for (var j = 0; j < 2; ++j)
            //{
            //var entity3 = world.CreateEntity();
            //entity3.AddComponent(new Transform2D { Position = Vector2.Zero, Scale = Vector2.One });
            //entity3.AddComponent(new Velocity { Value = new Vector3(random.Next(-5000, 5000) / 100f, random.Next(-5000, 5000) / 100f, 0) });
            //entity3.AddComponent(new Sprite { TextureCoordinates = TextureCoordinates.Default, Color = new Color(1, 1, 1) });
            //entity3.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\link.png"));
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


            // set up the player with a camera
            {
                var player = world.CreateEntity();
                player.AddComponent(new Transform3D{Rotation = Quaternion.Identity});
                player.AddComponent(new Player());
                
                var camera = world.CreateEntity();
                camera.AddComponent(new Camera { Up = Vector3.UnitY, Forward = Vector3.UnitZ, Width = 1f, Height = display.Window.Height / (float)display.Window.Width, NearPlane = 0.5f, FarPlane = 1000f });
                camera.AddComponent(new Transform3D());
                camera.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
                player.Attach(camera);
            }

            {
                var sphere = world.CreateEntity();
                sphere.AddComponent(new Transform3D { Position = new Vector3(-3f, 0, 2f), Scale = new Vector3(1f) });
                sphere.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\table.obj"));
                sphere.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\red.png"));
            }


            for (var i = 0; i < 600; ++i)
            {
                var sphere = world.CreateEntity();
                const float distaneConstant = 100f;
                sphere.AddComponent(new Transform3D { Position = new Vector3(random.Next(-10000, 10000)/distaneConstant, random.Next(-10000, 10000) / distaneConstant, random.Next(-10000, 10000) / distaneConstant), Scale = new Vector3(random.Next(100, 300)/100f) });
                sphere.AddComponent(new Velocity{Value = new Vector3(random.Next(-10000, 10000) / 1000f, random.Next(-10000, 10000) / 1000f ,random.Next(-10000, 10000) / 1000f)});
                switch (random.Next(10) % 3)
                {
                    case 0: sphere.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere1.obj")); break;
                    case 1: sphere.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj")); break;
                    case 2: sphere.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\cube.obj")); break;
                }

                sphere.AddComponent(random.Next(10) % 2 == 0
                    ? new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\blue.png")
                    : new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\red.png"));
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
                light.AddComponent(new Transform3D { Position = new Vector3(1f, 1f, 0), Scale = new Vector3(0.3f)});
                light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\white.png"));
                light.AddComponent(new Velocity{Value = new Vector3{X = -4.5f, Y = 6f}});
            }
            
            {
                var light = world.CreateEntity();
                light.AddComponent(new Light{Color = new Color(1, 1f, 1f)});
                light.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0), Scale = new Vector3(0.3f)});
                light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\white.png"));
                light.AddComponent(new Velocity{Value = new Vector3{X = 2.5f, Y = 3f}});
            }
            
            {
                var light = world.CreateEntity();
                light.AddComponent(new Light{Color = new Color(1, 1f, 1f)});
                light.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0), Scale = new Vector3(0.3f)});
                light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\white.png"));
                light.AddComponent(new Velocity{Value = new Vector3{X = 3.5f, Y = -8f, Z = 3f}});
            }
            //{
            //    var light = world.CreateEntity();
            //    light.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
            //    light.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0), Scale = new Vector3(0.3f) });
            //    light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
            //    light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\white.png"));
            //    light.AddComponent(new Velocity { Value = new Vector3 { X = -3.5f, Y = -3f, Z = 5f } });
            //}
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

            //var world = builder
            //    .WithComponent<Transform2D>()
            //    .WithComponent<Transform3D>()
            //    .WithComponent<TransformRect>()
            //    .WithComponent<Light>()
            //    .WithComponent<Velocity>()
            //    .WithComponent<Sprite>()
            //    .WithComponent<Camera>(10)
            //    .WithComponent<Model3D>()
            //    .WithComponent<Texture2D>()

            //    // TODO: Add a better way to handle resources
            //    .WithComponent<Resource<string, ITexture2D>>()
            //    .WithComponent<Resource<string, IMesh>>()
                
                
            //    //.WithComponent<VertexShader>()
            //    //.WithComponent<PixelShader>()
            //    .Build();

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
