using System;

namespace Titan.ECS3.Systems
{
    /// <summary>
    /// Base system that does logic on a single component
    /// </summary>
    public abstract class ComponentSystem<T> : ISystem where T : struct
    {
        private readonly IWorld _world;
        protected ComponentSystem(IWorld world)
        {
            _world = world;
        }

        public void PreUpdate()
        {
            throw new NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            OnPreUpdate();
            foreach (var entity in new uint[10])
            {
                T component = default;
                OnUpdate(deltaTime, ref component);
            }
            OnPostUpdate();
        }

        protected virtual void OnPreUpdate(){}
        protected abstract void OnUpdate(float deltaTime, ref T component);
        protected virtual void OnPostUpdate(){}

        public void Dispose()
        {
            // noop 
        }
    }
}
