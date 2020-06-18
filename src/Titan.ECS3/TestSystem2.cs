using System;
using Titan.ECS3.Systems;

namespace Titan.ECS3
{
    public class TestSystem2 : EntitySystem
    {
        public TestSystem2(IWorld world) : base(world)
        {
        }

        protected override void OnUpdate(float deltaTime, ReadOnlySpan<uint> entities)
        {
            Console.WriteLine("UPDATE!");
            Console.WriteLine($"Entities: {entities.Length}");
        }
    }
}
