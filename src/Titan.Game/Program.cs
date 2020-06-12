using System;
using System.Diagnostics;
using System.Numerics;
using Titan.Components;
using Titan.Core.Ioc;
using Titan.ECS.World;
using Titan.ECS2;
using Titan.ECS2.Components;
using Titan.ECS2.Entities;
using Titan.ECS2.Systems;

namespace Titan.Game
{

    public class TestSystem : EntitySystem
    {
        private IComponentMapper<Transform2D> _sample1;
        private IComponentMapper<Transform3D> _sample2;

        public TestSystem(World world) : base(world.CreateEntitySetBuilder().With<Transform2D>().With<Transform3D>().Build())
        {
            _sample1 = world.GetComponentMapper<Transform2D>();
            _sample2 = world.GetComponentMapper<Transform3D>();
        }


        public override void Update(float deltaTime)
        {
            foreach (var entity in GetEntities())
            {
                ref var s = ref _sample1[entity];
                ref var s1 = ref _sample2[entity];

                var s1Position = s1.Position;
                s.Position += new Vector2(s1Position.X, s1Position.Y);
            }
        }
    }
    internal class Program : Application<Program>
    {


        private MyTestClass _test;
        static void Main(string[] args)
        {
            
            {
                var world = new WorldBuilder(maxEntities: 20_000)
                    .WithComponent<Transform2D>(11_000)
                    .WithComponent<Transform3D>(11_000)
                    .Build();

                var system = new TestSystem(world);

                var s = Stopwatch.StartNew();
                for (var i = 0; i < 10_000; i++)
                {
                    var entity1 = world.CreateEntity();
                    entity1.Add<Transform2D>();
                    entity1.Add<Transform3D>();
                }
                s.Stop();
                Console.WriteLine($"Creation: {s.Elapsed.TotalMilliseconds} ms");

                s.Restart();
                for (var i = 0; i < 100_000; i++)
                {
                    system.Update(0.1f);
                }
                s.Stop();
                Console.WriteLine($"Elapsed: {s.Elapsed.TotalMilliseconds} ms {s.Elapsed.TotalMilliseconds/ 100_000.0}");
                    //ref var t = ref entity1.Get<Transform2D>();
                    //t.Rotation = 10f;
                    //var entity2 = world.CreateEntity();
                    //entity2.Add(new Transform3D { Position = new Vector3(1, 2, 3), Scale = Vector3.One });

                    system.Update(0.1f);
                system.Update(0.1f);
                system.Update(0.1f);
                system.Update(0.1f);
                system.Update(0.1f);
                system.Update(0.1f);
                
                
                //entity2.Destroy();


                //ref var t1 = ref entity1.Get<Transform2D>();
                world.Destroy();;
                GC.Collect();

                
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
