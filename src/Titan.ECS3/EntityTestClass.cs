using System;
using Titan.Core.Ioc;
using Titan.ECS3.Systems;

namespace Titan.ECS3
{
    public class EntityTestClass
    {
        public void Run(IContainer container)
        {


            
            var worldContainer = container.CreateChildContainer();
            using var world = new WorldBuilder()
                .Build();

            var systemRunner = new SystemsRunnerBuilder(world, worldContainer)
                .WithSystem<TestSystem1>()
                .WithSystem<TestSystem2>()
                .Build();


            var entity = world.CreateEntity();

            var entity1 = world.CreateEntity();
            var entity2 = world.CreateEntity();
            var entity3 = world.CreateEntity();
            var entity4 = world.CreateEntity();
            entity.Destroy();

            systemRunner.Update(1f);

        }
    }
}
