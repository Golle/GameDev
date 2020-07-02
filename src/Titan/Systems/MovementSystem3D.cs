using System.Numerics;
using Titan.Components;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;

namespace Titan.Systems
{
    internal class MovementSystem3D : EntitySystem
    {
        private readonly IComponentMap<Transform3D> _transform;
        private readonly IComponentMap<Velocity> _velocity;

        public MovementSystem3D(IWorld world)
            : base(world, world.EntityFilter().With<Transform3D>().With<Velocity>())
        {
            _transform = Map<Transform3D>();
            _velocity = Map<Velocity>();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var velocity = ref _velocity[entityId].Value;
            if (velocity != Vector3.Zero)
            {
                ref var transform = ref _transform[entityId];
                if (transform.Position.X > 3f || transform.Position.X < -3f)
                {
                    velocity.X *= -1f;
                }
                if (transform.Position.Y > 3f || transform.Position.Y < -3f)
                {
                    velocity.Y *= -1f;
                }
                transform.Position += velocity * deltaTime;
            }
        }
    }
}
