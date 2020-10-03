using System;
using System.Numerics;
using Titan.Components;
using Titan.Components.UI;
using Titan.Core.Math;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.Runners;
using Titan.ECS.Systems;
using Titan.Graphics.Fonts;
using Titan.Graphics.Models;
using Titan.Graphics.Textures;
using Titan.Sound;
using Titan.Windows.Window;

namespace Titan.Game
{
    internal class GameWorld : IWorldBuilder
    {
        private const string SpriteSheet = @"ui_spritesheet.png";
        private const string FontFilename = @"segoe_ui_light.fnt";
        private const string BackgroundMusic = @"speck_-_Hydrogen_Sky0.wav";
        private const string ClickSound = @"click_004.wav";
        private static readonly TextureCoordinates ButtonCoordinates = new TextureCoordinates { BottomRight = new Vector2(1f / 8f, 1f - 1 / 16f), TopLeft = new Vector2(0, 1f) };
        private static readonly TextureCoordinates GrayBox = new TextureCoordinates { BottomRight = new Vector2(1f / 4f, 1f- 1 / 16f), TopLeft = new Vector2(1/8f, 1f) };

        private readonly IWindow _window;

        public GameWorld(IWindow window)
        {
            _window = window;
        }

        public string SceneDescriptor() => @"F:\Git\GameDev\src\Titan.Game\Scenes\scene01.json";
        public SystemsRunnerBuilder ConfigureSystems(SystemsRunnerBuilder builder)
        {
            return builder
                .WithSystem<PlayerControllerSystem>()
                .WithSystem<SoundTestSystem>();
        }
        
        public IWorld CreateWorld(IWorld world)
        {

            // set up the player with a camera
            {
                var player = world.CreateEntity();
                player.AddComponent(new Transform3D { Rotation = Quaternion.Identity });
                player.AddComponent(new Player());

                var camera = world.CreateEntity();
                camera.AddComponent(new Camera { Up = Vector3.UnitY, Forward = Vector3.UnitZ, Width = 1f, Height = _window.Height / (float)_window.Width, NearPlane = 0.5f, FarPlane = 10000f });
                camera.AddComponent(new Transform3D());
                camera.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
                player.Attach(camera);
            }

            // add sound component
            {
                var sound = world.CreateEntity();
                sound.AddComponent(new Resource<string, ISoundClip>(BackgroundMusic));
            }
            {
                var sound = world.CreateEntity();
                sound.AddComponent(new Resource<string, ISoundClip>(ClickSound));
            }

            // set up the UI
            {
                var center = world.CreateEntity();
                center.AddComponent(new TransformRectComponent { AnchorPoint = AnchorPoint.Center, Offset = Vector3.UnitY*100, Size = new Size(140, 70) });
                center.AddComponent(new Sprite { TextureCoordinates = ButtonCoordinates, Color = Color.Green });
                center.AddComponent(new Resource<string, ITexture2D>(SpriteSheet));
                
                var bottom = world.CreateEntity();
                bottom.AddComponent(new TransformRectComponent { AnchorPoint = AnchorPoint.Bottom, Offset = Vector3.Zero, Size = new Size(140, 70) });
                bottom.AddComponent(new Sprite { TextureCoordinates = ButtonCoordinates, Color = Color.Red });
                bottom.AddComponent(new Resource<string, ITexture2D>(SpriteSheet));


                var loremText = world.CreateEntity();
                loremText.AddComponent(new UITextComponent("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", fontSize: 24, color: Color.Black));
                loremText.AddComponent(new TransformRectComponent { AnchorPoint = AnchorPoint.Center, Offset = Vector3.UnitY * -200f, Size = new Size(600, 600) });
                loremText.AddComponent(new Sprite { TextureCoordinates = GrayBox, Color = Color.White });
                loremText.AddComponent(new Resource<string, IFont>(FontFilename));
                loremText.AddComponent(new Resource<string, ITexture2D>(SpriteSheet));

                var anotherText = world.CreateEntity();
                anotherText.AddComponent(new UITextComponent("Bottom text woop woop!", fontSize: 42, color: Color.Red));
                anotherText.AddComponent(new TransformRectComponent { AnchorPoint = AnchorPoint.BottomRight, Size = new Size(250, 120) });
                anotherText.AddComponent(new Resource<string, IFont>(FontFilename));
                //anotherText.AddComponent(new Resource<string, ITexture2D>(SpriteSheet));
                //anotherText.AddComponent(new Sprite { TextureCoordinates = ButtonCoordinates, Color = Color.White });
                loremText.Attach(anotherText);

                //{
                //    var text2 = world.CreateEntity();
                //    text2.AddComponent(new UITextComponent(str1, Color.Red));
                //    text2.AddComponent(new TransformRectComponent { AnchorPoint = AnchorPoint.Left, Offset = Vector3.UnitY * 300f, Size = new Size(300, 200) });
                //    text2.AddComponent(new Resource<string, IFont>(FontFilename));
                //}
                //{
                //    var text2 = world.CreateEntity();
                //    text2.AddComponent(new UITextComponent(str2, Color.Blue));
                //    text2.AddComponent(new TransformRectComponent { AnchorPoint = AnchorPoint.Left, Offset = Vector3.UnitY * 100f, Size = new Size(300, 200) });
                //    text2.AddComponent(new Resource<string, IFont>(FontFilename));
                //}
                //{
                //    var text2 = world.CreateEntity();
                //    text2.AddComponent(new UITextComponent(str3, Color.Green));
                //    text2.AddComponent(new TransformRectComponent { AnchorPoint = AnchorPoint.Left, Offset = Vector3.UnitY * -100f, Size = new Size(300, 200) });
                //    text2.AddComponent(new Resource<string, IFont>(FontFilename));
                //}
                //{
                //    var text2 = world.CreateEntity();
                //    text2.AddComponent(new UITextComponent(str4, Color.White));
                //    text2.AddComponent(new TransformRectComponent { AnchorPoint = AnchorPoint.Left, Offset = Vector3.UnitY * -300f, Size = new Size(300, 200) });
                //    text2.AddComponent(new Resource<string, IFont>(FontFilename));
                //}


                //var text3 = world.CreateEntity();
                //text3.AddComponent(new UITextComponent("Fjupp", Color.Red));
                //text3.AddComponent(new TransformRectComponent { AnchorPoint = AnchorPoint.Left, Offset = Vector3.UnitY * 0f, Size = new Size(300, 200) });
                //text3.AddComponent(new Resource<string, IFont>(FontFilename));
                //text3.AddComponent(new Sprite { TextureCoordinates = ButtonCoordinates, Color = Color.Green });
                //text3.AddComponent(new Resource<string, ITexture2D>(SpriteSheet));

                //mainEntity.Attach(topLeftButton);

                //var bottomRight = world.CreateEntity();
                //bottomRight.AddComponent(new TransformRect { AnchorPoint = AnchorPoint.TopRight, Offset = new Vector3(-10, -10, 0), Size = new Size(140, 70) });
                //bottomRight.AddComponent(new Sprite { TextureCoordinates = ButtonCoordinates, Color = Color.Blue });
                //bottomRight.AddComponent(new Resource<string, ITexture2D>(SpriteSheet));

                //mainEntity.Attach(bottomRight);

                //var right = world.CreateEntity();
                //right.AddComponent(new TransformRect { AnchorPoint = AnchorPoint.Right, Offset = new Vector3(-100, 0, 0), Size = new Size(140, 70) });
                //right.AddComponent(new Sprite { TextureCoordinates = ButtonCoordinates, Color = Color.Green });
                //right.AddComponent(new Resource<string, ITexture2D>(SpriteSheet));
                //mainEntity.Attach(right);



                //mainEntity.Attach(center);
            }


            // set up game world
            {

                //{
                //    var sponza = world.CreateEntity();
                //    sponza.AddComponent(new Resource<string, IMesh>(@"sponza/sponza.obj"));
                //}

                var random = new Random();
                //{
                //    var sphere = world.CreateEntity();
                //    sphere.AddComponent(new Transform3D { Position = new Vector3(-3f, 0, 2f), Scale = new Vector3(1f) });
                //    sphere.AddComponent(new Resource<string, IMesh>(@"table.dat"));
                //    sphere.AddComponent(new Resource<string, ITexture2D>(@"red.png"));
                //}

                {
                    var sponza = world.CreateEntity();
                    sponza.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0f), Scale = Vector3.One });
                    sponza.AddComponent(new Resource<string, IMesh>(@"sponza.dat"));
                    sponza.AddComponent(new Resource<string, ITexture2D>(@"temp_models/sponza/textures/spnza_bricks_a_diff.png")); // temp
                }

                //{
                //    var stone = world.CreateEntity();
                //    stone.AddComponent(new Transform3D { Position = new Vector3(0f, 10f, 10f), Scale = Vector3.One*3 });
                //    stone.AddComponent(new Resource<string, MeshGroup>(@"stone.dat"));
                //    stone.AddComponent(new Resource<string, ITexture2D>(@"stone/diffuso.tif"));
                //}
                //{
                //    var door = world.CreateEntity();
                //    door.AddComponent(new Transform3D { Position = new Vector3(0f, 10f, 10f), Scale = Vector3.One*3 });
                //    door.AddComponent(new Resource<string, MeshGroup>(@"Door.dat"));
                //    door.AddComponent(new Resource<string, ITexture2D>(@"door/WoodTexture.jpg"));
                //}

                //{
                //    var clock = world.CreateEntity();
                //    clock.AddComponent(new Transform3D { Position = new Vector3(0f, 10f, 10f), Scale = Vector3.One*3 });
                //    clock.AddComponent(new Resource<string, MeshGroup>(@"Clock_obj.dat"));
                //    clock.AddComponent(new Resource<string, ITexture2D>(@"Clock_Constraints_for_Animations\textures\Uhr_ohne_Zeiger.jpg"));
                //}


                for (var i = 0; i < 100; ++i)
                {
                    var sphere = world.CreateEntity();
                    const float distaneConstant = 100f;
                    sphere.AddComponent(new Transform3D { Position = new Vector3(random.Next(-10000, 10000) / distaneConstant, random.Next(-10000, 10000) / distaneConstant, random.Next(-10000, 10000) / distaneConstant), Scale = new Vector3(random.Next(100, 300) / 100f) });
                    sphere.AddComponent(new Velocity { Value = new Vector3(random.Next(-10000, 10000) / 1000f, random.Next(-10000, 10000) / 1000f, random.Next(-10000, 10000) / 1000f) });
                    switch (random.Next(10) % 3)
                    {
                        case 0: sphere.AddComponent(new Resource<string, IMesh>(@"sphere1.dat")); break;
                        case 1: sphere.AddComponent(new Resource<string, IMesh>(@"sphere.dat")); break;
                        case 2: sphere.AddComponent(new Resource<string, IMesh>(@"cube.dat")); break;
                    }

                    sphere.AddComponent(random.Next(10) % 2 == 0
                        ? new Resource<string, ITexture2D>(@"blue.png")
                        : new Resource<string, ITexture2D>(@"red.png"));
                }

                {
                    var sphere = world.CreateEntity();
                    sphere.AddComponent(new Transform3D { Position = new Vector3(0, 3f, 2f), Scale = new Vector3(1f) });
                    sphere.AddComponent(new Resource<string, IMesh>(@"cube.dat"));
                    sphere.AddComponent(new Resource<string, ITexture2D>(@"red.png"));
                }

                {
                    var sphere = world.CreateEntity();
                    sphere.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 7f), Scale = new Vector3(10f, 10f, 1f) });
                    sphere.AddComponent(new Resource<string, IMesh>(@"cube.dat"));
                    sphere.AddComponent(new Resource<string, ITexture2D>(@"blue.png"));
                }

                {
                    var light = world.CreateEntity();
                    light.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
                    light.AddComponent(new Transform3D { Position = new Vector3(1f, 1f, 0), Scale = new Vector3(0.3f) });
                    light.AddComponent(new Resource<string, IMesh>(@"sphere.dat"));
                    light.AddComponent(new Resource<string, ITexture2D>(@"white.png"));
                    light.AddComponent(new Velocity { Value = new Vector3 { X = -4.5f, Y = 6f } });
                }

                {
                    var light = world.CreateEntity();
                    light.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
                    light.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0), Scale = new Vector3(0.3f) });
                    light.AddComponent(new Resource<string, IMesh>(@"sphere.dat"));
                    light.AddComponent(new Resource<string, ITexture2D>(@"white.png"));
                    light.AddComponent(new Velocity { Value = new Vector3 { X = 2.5f, Y = 3f } });
                }

                {
                    var light = world.CreateEntity();
                    light.AddComponent(new Light { Color = new Color(1, 1f, 1f) });
                    light.AddComponent(new Transform3D { Position = new Vector3(0f, 0f, 0), Scale = new Vector3(0.3f) });
                    light.AddComponent(new Resource<string, IMesh>(@"sphere.dat"));
                    light.AddComponent(new Resource<string, ITexture2D>(@"white.png"));
                    light.AddComponent(new Velocity { Value = new Vector3 { X = 3.5f, Y = -8f, Z = 3f } });
                }
            }

            return world;
        }
    }
}
