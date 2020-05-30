using System.Numerics;
using Titan.Components;
using Titan.ECS.Components;
using Titan.ECS.Systems;

namespace Titan.Systems
{
    internal class Transform3DSystem : BaseSystem
    {
        private readonly IComponentMapper<Transform3D> _transform;
        
        public Transform3DSystem(IComponentManager componentManager)
        : base(typeof(Transform3D))
        {
            _transform = componentManager.GetComponentMapper<Transform3D>();
        }

        protected override void OnUpdate(float deltaTime)
        {

            // TODO: add relationship handling, query and order parents to calculate the world position
            foreach (var entity in Entities)
            {
                ref var transform = ref _transform[entity];
                transform.ModelTransform =
                    Matrix4x4.CreateTranslation(transform.Position) *
                    Matrix4x4.CreateScale(transform.Scale) *
                    Matrix4x4.CreateFromQuaternion(transform.Rotation);

                transform.WorldTransform = Matrix4x4.Transpose(transform.ModelTransform);
            }
            
        }
        
    }
}
