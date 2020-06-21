using System;
using Titan.ECS.Components;

namespace Titan.ECS.Systems
{
    /// <summary>
    /// Base system that does logic on a single component
    /// </summary>
    public abstract class ComponentSystem<T> : ISystem where T : struct
    {
        private readonly bool _skipUpdateIfEmpty;
        private readonly IComponentMap<T> _components;
        protected ComponentSystem(IWorld world, bool skipUpdateIfEmpty = false)
        {
            _skipUpdateIfEmpty = skipUpdateIfEmpty;
            _components = world.GetComponentMap<T>();
        }

        public void Update(float deltaTime)
        {
            var components = _components.AsSpan();
            if (components.IsEmpty && _skipUpdateIfEmpty)
            {
                return;
            }

            OnPreUpdate();
            for(var i = 0; i < components.Length; ++i)
            {
                OnUpdate(deltaTime, ref components[i]);
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
