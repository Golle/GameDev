using Titan.Components;
using Titan.ECS;
using Titan.ECS.Systems;

namespace Titan.Systems
{
    internal class Transform2DSystem : ComponentSystem<Transform2D>
    {
        public Transform2DSystem(IWorld world) 
            : base(world, true)
        {
        }

                //ref var transform = ref _transform[entity];
                //ref var relationship = ref _relationship[entity];
                //if(_transform.Exists(relationship.Parent))
                //{
                //    ref var parentTransform = ref _transform[relationship.Parent];
                //    transform.WorldPosition = transform.Position + parentTransform.Position;
                //}
                //else
                //{
                //    transform.WorldPosition = transform.Position;
                //}

        protected override void OnUpdate(float deltaTime, ref Transform2D component)
        {
            // TODO: relationships.. DOh
            
            component.WorldPosition = component.Position;
        }
    }
}
