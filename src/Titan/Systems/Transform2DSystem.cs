using Titan.Components;
using Titan.ECS.Components;
using Titan.ECS.Systems;

namespace Titan.Systems
{
    internal class Transform2DSystem : BaseSystem
    {
        private readonly IComponentMapper<Transform2D> _transform;
        private readonly IComponentMapper<Relationship> _relationship;

        public Transform2DSystem(IComponentManager componentManager)
            : base(typeof(Transform2D))
        {
            _transform = componentManager.GetComponentMapper<Transform2D>();
            _relationship = componentManager.GetComponentMapper<Relationship>();
        }

        protected override void OnUpdate(float deltaTime)
        {
            // TODO: add relationship handling, query and order parents to calculate the world position
            foreach (var entity in Entities)
            {
                ref var transform = ref _transform[entity];
                ref var relationship = ref _relationship[entity];
                if(_transform.Exists(relationship.Parent))
                {
                    ref var parentTransform = ref _transform[relationship.Parent];
                    transform.WorldPosition = transform.Position + parentTransform.Position;
                }
                else
                {
                    transform.WorldPosition = transform.Position;
                }
            }
        }
    }
}
