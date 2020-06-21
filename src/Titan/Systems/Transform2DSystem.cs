using Titan.Components;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;

namespace Titan.Systems
{
    /// <summary>
    /// Transform2D with parents (single level)
    /// </summary>
    internal class Transform2DEntitySystem : EntitySystem
    {
        private readonly IComponentMap<Transform2D> _transform;
        private readonly IRelationship _relationship;

        // TODO: Add support for sorting the entities based on parent count
        public Transform2DEntitySystem(IWorld world) 
            : base(world, world.EntityFilter().With<Transform2D>())
        {
            _transform = Map<Transform2D>();
            _relationship = Relationship();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var transform = ref _transform[entityId];
            if (_relationship.TryGetParent(entityId, out var parentId) && _transform.Has(parentId))
            {
                transform.WorldPosition = transform.Position + _transform[parentId].WorldPosition;
            }
            else
            {
                transform.WorldPosition = transform.Position;
            }
        }
    }

    /// <summary>
    /// Transform2D without parents
    /// </summary>
    internal class Transform2DComponentSystem : ComponentSystem<Transform2D>
    {
        public Transform2DComponentSystem(IWorld world) 
            : base(world, true)
        {
        }
        
        protected override void OnUpdate(float deltaTime, ref Transform2D component)
        {
            component.WorldPosition = component.Position;
        }
    }
}
