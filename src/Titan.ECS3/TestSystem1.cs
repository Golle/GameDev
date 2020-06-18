using System;
using Titan.ECS3.Systems;

namespace Titan.ECS3
{
    public class TestSystem1 : ComponentSystem<SampleComponent>
    {
        public TestSystem1(IWorld world) : base(world)
        {

        }

        protected override void OnUpdate(float deltaTime, ref SampleComponent component)
        {
            Console.WriteLine("Got teh components!");
        }
    }
}
