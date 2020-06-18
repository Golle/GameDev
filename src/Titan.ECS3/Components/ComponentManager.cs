using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Titan.ECS3.Components
{
    internal class ComponentManager
    {
        private readonly uint _maxEntities;

        private readonly IDictionary<ulong, object> _componentMaps = new Dictionary<ulong, object>();

        public ComponentManager(uint maxEntities)
        {
            _maxEntities = maxEntities;
        }
        
        public void RegisterComponent<T>(uint size) where T : struct
        {
            Debug.Assert(_componentMaps.ContainsKey(ComponentId<T>.Id) == false, $"Component {typeof(T).Name} has already been registered");
            _componentMaps[ComponentId<T>.Id] = new ComponentMap<T>(_maxEntities, size);
        }

        public void RegisterComponent(Type type, uint size)
        {
            var componentId = (ulong) typeof(ComponentId<>).MakeGenericType(type).GetProperty("Id").GetValue(null);
            Debug.Assert(_componentMaps.ContainsKey(componentId) == false, $"Component {type.Name} has already been registered");
            _componentMaps[componentId] = Activator.CreateInstance(typeof(ComponentMap<>).MakeGenericType(type), _maxEntities, size); ;
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<T>(uint entityId) where T : struct => ((ComponentMap<T>)_componentMaps[ComponentId<T>.Id]).Add(entityId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<T>(uint entityId, in T value) where T : struct => ((ComponentMap<T>)_componentMaps[ComponentId<T>.Id]).Add(entityId, value);
    }
}
