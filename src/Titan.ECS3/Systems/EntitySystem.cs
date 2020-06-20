using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Titan.ECS3.Systems
{
    /// <summary>
    /// Base system that does logic on a set of Components (entities)
    /// </summary>
    public abstract class EntitySystem : ISystem
    {
        private readonly EntityFilter _filter;
        
        protected EntitySystem(IWorld world, EntityFilter filter)
        {
            _filter = filter;
        }

        public void Update(float deltaTime)
        {
            OnPreUpdate();
            var entities = _filter.GetEntities();
            for (var i = 0; i < entities.Length; ++i)
            {
                OnUpdate(deltaTime, entities[i]);
            }
            OnPostUpdate();
        }


        protected virtual void OnPreUpdate() { }
        protected abstract void OnUpdate(float deltaTime, uint entityId);
        protected virtual void OnPostUpdate() { }

        public void Dispose()
        {
            _filter.Dispose();
        }
    }
}