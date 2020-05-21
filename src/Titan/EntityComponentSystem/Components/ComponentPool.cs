using System.Collections.Generic;
using System.Diagnostics;
using Titan.Systems.Components;

namespace Titan.EntityComponentSystem.Components
{
    public class ComponentPool<T> : IComponentPool<T> where T : class, IComponent, new()
    {
        private readonly int _maxSize;
        private readonly bool _resetOnGet;

        private readonly Queue<T> _components;
        public ComponentPool(int initialSize, int maxSize, bool resetOnGet = true)
        {
            Debug.Assert(maxSize >= initialSize, $"{nameof(maxSize)} must be greater or equal to {nameof(initialSize)}");

            _maxSize = maxSize;
            _resetOnGet = resetOnGet;
            _components = new Queue<T>(initialSize);
            for (var i = 0; i < initialSize; ++i)
            {
                _components.Enqueue(new T());
            }
        }

        public T Get()
        {
            if (!_components.TryDequeue(out var component))
            {
                return new T();
            }
            if (_resetOnGet)
            {
                component.Reset();
            }
            return component;
        }

        public void Put(T component)
        {
            if (_components.Count <= _maxSize)
            {
                _components.Enqueue(component);
            }
        }
    }
}
