using System;
using System.Numerics;
using Titan.Components;
using Titan.Core.Ioc;
using Titan.ECS.World;
using Titan.ECS2;
using Titan.ECS2.Components;

namespace Titan.Game
{
    internal class Program : Application<Program>
    {


        private MyTestClass _test;
        static void Main(string[] args)
        {
            
            {
                var world = new WorldBuilder(maxEntities: 20_000)
                    .WithComponent<Transform2D>(10_000)
                    .WithComponent<Transform3D>(10_000)
                    .Build();
                
                var entity1 = world.CreateEntity();
                entity1.Add<Transform2D>();
                //entity1.Set(new Transform2D{Position = new Vector2(1,2), Scale =  Vector2.One});
                ref var t = ref entity1.Get<Transform2D>();
                t.Rotation = 10f;
                var entity2 = world.CreateEntity();
                entity2.Add(new Transform3D { Position = new Vector3(1, 2, 3), Scale = Vector3.One });

                entity2.Destroy();


                ref var t1 = ref entity1.Get<Transform2D>();
                world.Destroy();;
            }
            Start();
        }

        protected override void OnInitialize(IContainer container)
        {
            _test = container.GetInstance<MyTestClass>();
        }

        protected override void OnStart()
        {
            _test.OnStart();
        }

        protected override void OnQuit()
        {
            _test.OnQuit();
        }

        protected override void RegisterServices(IContainer container)
        {
            container.Register<MyTestClass>();
        }
    }

    internal class MyTestClass
    {
        
        public MyTestClass(IWorldCreator worldCreator)
        {
            
        }

        public void OnStart()
        {
            
            

            Console.WriteLine("OnStart");
        }

        public void OnQuit()
        {
            Console.WriteLine("OnQuit");
        }
    }
}
