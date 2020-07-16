using System.Numerics;
using Titan.Components;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;

namespace Titan.Systems
{
    internal class Transform3DEntitySystem : EntitySystem
    {
        private readonly IComponentMap<Transform3D> _transform;
        private readonly IRelationship _relationship;

        public Transform3DEntitySystem(IWorld world)
            : base(world, world.EntityFilter().With<Transform3D>())
        {
            _transform = Map<Transform3D>();
            _relationship = Relationship();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            // TODO: Use a dirty flag to avoid a massive amount of calculations

            ref var transform = ref _transform[entityId];
            transform.ModelTransform =
                Matrix4x4.CreateScale(transform.Scale) *
                Matrix4x4.CreateFromQuaternion(transform.Rotation) *
                Matrix4x4.CreateTranslation(transform.Position)
                ;

            if (_relationship.TryGetParent(entityId, out var parentId) && _transform.Has(parentId))
            {
                transform.WorldTransform = transform.ModelTransform * _transform[parentId].WorldTransform;
            }
            else
            {
                transform.WorldTransform = transform.ModelTransform;
            }
        }
    }
}
