using System;
using System.Numerics;
using Titan.Components;
using Titan.Core.Math;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.Runners;
using Titan.ECS.Systems;
using Titan.Graphics.Models;
using Titan.Graphics.Textures;
using Titan.Windows.Window;

namespace Titan.Game
{
    internal class GameWorld : IWorldBuilder
    {
        private const string SpriteSheet = @"F:\Git\GameDev\resources\ui_spritesheet.png";
        private static readonly TextureCoordinates ButtonCoordinates = new TextureCoordinates { BottomRight = new Vector2(1f / 8f, 1), TopLeft = new Vector2(0, 1f - 1f / 16f) };

        private readonly IWindow _window;

        public GameWorld(IWindow window)
        {
            _window = window;
        }

        public string SceneDescriptor() => @"F:\Git\GameDev\src\Titan.Game\Scenes\scene01.json";
        public SystemsRunnerBuilder ConfigureSystems(SystemsRunnerBuilder builder)
        {
            return builder.WithSystem<PlayerControllerSystem>();
        }

        public IWorld CreateWorld(IWorld world)
        {

            // set up the player with a camera
            {
                var player = world.CreateEntity();
                player.AddComponent(new Transform3D { Rotation = Quaternion.Identity });
                player.AddComponent(new Player());

                var camera = world.CreateEntity();
                camera.AddComponent(new Camera { Up = Vector3.UnitY, Forward = Vector3.UnitZ, Width = 1f, Height = _window.Height / (float)_window.Width, NearPlane = 0.5f, FarPlane = 1000f });
                camera.AddComponent(new Transform3D());
                camera.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
                player.Attach(camera);
            }


            // set up the UI
            {
                var mainEntity = world.CreateEntity();
                mainEntity.AddComponent(new TransformRect { Position = Vector3.Zero, Size = new Size(_window.Width, _window.Height) });

                var topLeftButton = world.CreateEntity();
                topLeftButton.AddComponent(new TransformRect { Position = Vector3.Zero, Size = new Size(140, 70) });
                topLeftButton.AddComponent(new Sprite { TextureCoordinates = ButtonCoordinates, Color = Color.White });
                topLeftButton.AddComponent(new Resource<string, ITexture2D>(SpriteSheet));

                mainEntity.Attach(topLeftButton);

                var bottomRight = world.CreateEntity();
                bottomRight.AddComponent(new TransformRect { Position = new Vector3(_window.Width - 140, _window.Height - 70, 0), Size = new Size(140, 70) });
                bottomRight.AddComponent(new Sprite { TextureCoordinates = ButtonCoordinates, Color = Color.White });
                bottomRight.AddComponent(new Resource<string, ITexture2D>(SpriteSheet));

                mainEntity.Attach(bottomRight);
            }


            // set up game world
            {
                var random = new Random();
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
                    sphere.AddComponent(new Transform3D { Position = new Vector3(random.Next(-10000, 10000) / distaneConstant, random.Next(-10000, 10000) / distaneConstant, random.Next(-10000, 10000) / distaneConstant), Scale = new Vector3(random.Next(100, 300) / 100f) });
                    sphere.AddComponent(new Velocity { Value = new Vector3(random.Next(-10000, 10000) / 1000f, random.Next(-10000, 10000) / 1000f, random.Next(-10000, 10000) / 1000f) });
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
                    light.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
                    light.AddComponent(new Transform3D { Position = new Vector3(1f, 1f, 0), Scale = new Vector3(0.3f) });
                    light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                    light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\white.png"));
                    light.AddComponent(new Velocity { Value = new Vector3 { X = -4.5f, Y = 6f } });
                }

                {
                    var light = world.CreateEntity();
                    light.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
                    light.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0), Scale = new Vector3(0.3f) });
                    light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                    light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\white.png"));
                    light.AddComponent(new Velocity { Value = new Vector3 { X = 2.5f, Y = 3f } });
                }

                {
                    var light = world.CreateEntity();
                    light.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
                    light.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0), Scale = new Vector3(0.3f) });
                    light.AddComponent(new Resource<string, IMesh>(@"F:\Git\GameDev\resources\sphere.obj"));
                    light.AddComponent(new Resource<string, ITexture2D>(@"F:\Git\GameDev\resources\white.png"));
                    light.AddComponent(new Velocity { Value = new Vector3 { X = 3.5f, Y = -8f, Z = 3f } });
                }
            }

            return world;
        }
    }
}
