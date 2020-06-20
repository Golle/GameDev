using System;
using Titan.Core.Ioc;
using Titan.ECS3.Components;
using Titan.ECS3.Systems;

namespace Titan.ECS3
{
    public class EntityTestClass
    {
        public void Run(IContainer container)
        {
            //var worldContainer = container.CreateChildContainer();
            //using var world = new WorldBuilder()
            //    .WithComponent<Transform2D>(100)
            //    .Build();

            //var systemRunner = new SystemsRunnerBuilder(world, worldContainer)
            //    .WithSystem<TestSystem1>()
            //    .WithSystem<TestSystem2>()
            //    .WithSystem<TestSystem3>()

            //    .Build();



            //var entity = world.CreateEntity();
            //entity.AddComponent<Transform2D>();

            //var entity1 = world.CreateEntity();
            //var entity2 = world.CreateEntity();
            //var entity3 = world.CreateEntity();
            //entity3.AddComponent<Transform2D>();
            //var entity4 = world.CreateEntity();
            //entity1.Attach(entity2);
            //entity2.Attach(entity3);
            //entity3.Attach(entity4);
            
            //entity2.Destroy();

            //systemRunner.Update(1f);

        }
    }
}
