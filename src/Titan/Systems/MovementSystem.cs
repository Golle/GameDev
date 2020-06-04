using System.Numerics;
using Titan.Components;
using Titan.ECS.Components;
using Titan.ECS.Systems;

namespace Titan.Systems
{
    internal class MovementSystem : BaseSystem
    {
        private readonly IComponentMapper<Transform3D> _tranform3D;
        private readonly IComponentMapper<Velocity> _velocity;

        public MovementSystem(IComponentManager componentManager) 
            : base(typeof(Transform3D), typeof(Velocity))
        {
            _tranform3D = componentManager.GetComponentMapper<Transform3D>();
            _velocity = componentManager.GetComponentMapper<Velocity>();
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                ref var velocity = ref _velocity[entity];
                if (velocity.Value != Vector3.Zero)
                {
                    ref var transform3D = ref _tranform3D[entity];
                    transform3D.Position += velocity.Value * deltaTime;
                }
            }
        }
    }
}
