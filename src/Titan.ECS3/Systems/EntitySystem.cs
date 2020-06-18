using System;

namespace Titan.ECS3.Systems
{
    /// <summary>
    /// Base system that does logic on a set of Components (entities)
    /// </summary>
    public abstract class EntitySystem : ISystem
    {
        protected EntitySystem(IWorld world)
        {
        }

        public void Update(float deltaTime)
        {
            OnPreUpdate();
            
            OnUpdate(deltaTime, GetEntities());
            
            OnPostUpdate();
        }

        protected virtual void OnPreUpdate() { }
        protected abstract void OnUpdate(float deltaTime, ReadOnlySpan<uint> entities);
        protected virtual void OnPostUpdate() { }

        protected ReadOnlySpan<uint> GetEntities() => new uint[10];
    }
}
