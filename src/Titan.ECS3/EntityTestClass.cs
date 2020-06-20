using System;
using Titan.Core.Ioc;
using Titan.ECS3.Components;
using Titan.ECS3.Systems;

namespace Titan.ECS3
{
    public class EntityTestClass
    {
        struct Transform45D
        {
            public uint A;
            public uint B;
        }
        
        struct Transform1D
        {
            public uint A;
            public uint B;
        }

        public void Run(IContainer container)
        {
            var worldContainer = container.CreateChildContainer();
            using var world = new WorldBuilder()
                .WithComponent<Transform1D>(10)
                .WithComponent<Transform45D>(100)
                .WithComponent<Transform2D>(100)
                .Build();

            var systemRunner = new SystemsRunnerBuilder(world, worldContainer)
                .WithSystem<TestSystem1>()
                .WithSystem<TestSystem2>()
                .Build();

            var entity11 = world.CreateEntity();
            entity11.AddComponent<Transform1D>(new Transform1D{A = 1});
            
            var entity12 = world.CreateEntity();
            entity12.AddComponent<Transform1D>(new Transform1D { A = 2 });
            
            var entity13 = world.CreateEntity();
            entity13.AddComponent<Transform1D>(new Transform1D { A = 3 });
            
            entity13.RemoveComponent<Transform1D>();
            entity13.AddComponent<Transform1D>();
            
            



            var entity = world.CreateEntity();
            entity.AddComponent<Transform2D>();
            entity.AddComponent(new Transform45D());
            entity.AddComponent<Transform1D>();

            var entity1 = world.CreateEntity();
            var entity2 = world.CreateEntity();
            var entity3 = world.CreateEntity();
            entity3.AddComponent<Transform2D>();
            var entity4 = world.CreateEntity();
            entity1.Attach(entity2);
            entity2.Attach(entity3);
            entity3.Attach(entity4);
            
            entity2.Destroy();

            systemRunner.Update(1f);

        }
    }
}
