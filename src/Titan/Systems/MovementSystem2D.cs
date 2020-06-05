using System.Numerics;
using Titan.Components;
using Titan.ECS.Components;
using Titan.ECS.Systems;

namespace Titan.Systems
{
    internal class MovementSystem2D : BaseSystem
    {
        private readonly IComponentMapper<Transform2D> _tranform;
        private readonly IComponentMapper<Velocity> _velocity;

        public MovementSystem2D(IComponentManager componentManager)
            : base(typeof(Transform2D), typeof(Velocity))
        {
            _tranform = componentManager.GetComponentMapper<Transform2D>();
            _velocity = componentManager.GetComponentMapper<Velocity>();
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                ref var velocity = ref _velocity[entity].Value;
                if (velocity != Vector3.Zero)
                {
                    ref var transform = ref _tranform[entity];
                    transform.Position += new Vector2(velocity.X * deltaTime, velocity.Y * deltaTime);
                }
            }
        }
    }
}
