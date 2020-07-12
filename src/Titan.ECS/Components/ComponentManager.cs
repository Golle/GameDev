using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Titan.ECS.Messaging;

namespace Titan.ECS.Components
{
    internal class ComponentManager
    {
        private readonly uint _maxEntities;
        private readonly Publisher _publisher;

        private readonly IDictionary<ulong, object> _componentMaps = new Dictionary<ulong, object>();

        public ComponentManager(uint maxEntities, Publisher publisher)
        {
            _maxEntities = maxEntities;
            _publisher = publisher;
        }
        
        public void RegisterComponent<T>(uint size) where T : struct
        {
            Debug.Assert(_componentMaps.ContainsKey(ComponentId<T>.Id) == false, $"Component {typeof(T).Name} has already been registered");
            _componentMaps[ComponentId<T>.Id] = new ComponentPool<T>(_maxEntities, size, _publisher);
        }

        public void RegisterComponent(Type type, uint size)
        {
            var componentId = (ulong) typeof(ComponentId<>).MakeGenericType(type).GetProperty("Id").GetValue(null);
            Debug.Assert(_componentMaps.ContainsKey(componentId) == false, $"Component {type.Name} has already been registered");
            _componentMaps[componentId] = Activator.CreateInstance(typeof(ComponentPool<>).MakeGenericType(type), _maxEntities, size, _publisher); ;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<T>(uint entityId) where T : struct => ((ComponentPool<T>)_componentMaps[ComponentId<T>.Id]).Add(entityId);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<T>(uint entityId, in T value) where T : struct => ((ComponentPool<T>)_componentMaps[ComponentId<T>.Id]).Add(entityId, value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove<T>(uint entityId) where T : struct => ((ComponentPool<T>)_componentMaps[ComponentId<T>.Id]).Remove(entityId);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IComponentMap<T> Map<T>() where T : struct => (IComponentMap<T>)_componentMaps[ComponentId<T>.Id];

    }

}
