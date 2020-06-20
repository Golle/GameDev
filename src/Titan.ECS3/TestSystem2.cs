using System;
using Titan.ECS3.Systems;

namespace Titan.ECS3
{

    public class TestSystem3 : ComponentSystem<Transform2D>
    {
        public TestSystem3(IWorld world) : base(world)
        {
        }

        protected override void OnUpdate(float deltaTime, ref Transform2D component)
        {
            throw new NotImplementedException();
        }
    }

    public class TestSystem2 : EntitySystem
    {
        public TestSystem2(IWorld world) : base(world, world.EntityFilter().With<uint>())
        {
        }
        
        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            Console.WriteLine("UPDATE!");
        }
    }
}
