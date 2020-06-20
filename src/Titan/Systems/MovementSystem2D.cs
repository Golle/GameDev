using System.Numerics;
using Titan.Components;
using Titan.ECS3;
using Titan.ECS3.Components;
using Titan.ECS3.Systems;

namespace Titan.Systems
{
    internal class MovementSystem2D : EntitySystem
    {
        private readonly IComponentMap<Transform2D> _transform;
        private readonly IComponentMap<Velocity> _velocity;

        public MovementSystem2D(IWorld world) 
            : base(world, world.EntityFilter().With<Transform2D>().With<Velocity>())
        {
            _transform = Map<Transform2D>();
            _velocity = Map<Velocity>();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var velocity = ref _velocity[entityId].Value;
            if (velocity != Vector3.Zero)
            {
                ref var transform = ref _transform[entityId];
                transform.Position += new Vector2(velocity.X * deltaTime, velocity.Y * deltaTime);
            }
        }
    }
}
