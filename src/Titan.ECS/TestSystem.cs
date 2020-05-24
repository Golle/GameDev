using System;
using System.Numerics;
using Titan.ECS.Components;
using Titan.ECS.Systems;

namespace Titan.ECS
{
    public class TestSystem : BaseSystem
    {
        private readonly IComponentMapper<TestComponent1> _testComponent1;
        private readonly IComponentMapper<TestComponent2> _testComponent2;
        
        public TestSystem(IComponentManager componentManager) 
            : base(typeof(TestComponent2), typeof(TestComponent1))
        {
            _testComponent1 = componentManager.GetComponentMapper<TestComponent1>();
            _testComponent2 = componentManager.GetComponentMapper<TestComponent2>();
        }

        protected override void Update(float deltaTime, uint entity)
        {
            ref var comp1 = ref _testComponent1[entity];
            ref var comp2 = ref _testComponent2[entity];

            comp1.Transform = comp2.Transform * Matrix4x4.Identity;

            Console.WriteLine("Woop!");
            //Console.WriteLine("Woop!");
        }

    }
}
