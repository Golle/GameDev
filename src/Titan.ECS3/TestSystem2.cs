using System;
using Titan.ECS3.Systems;

namespace Titan.ECS3
{
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
