using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Titan.ECS3.Components
{
    internal sealed class ComponentPool<T> where T : struct
    {
        private readonly T[] _components;
        private readonly int[] _entityMap;

        private int _lastIndex = -1;
        public ComponentPool(uint maxEntities, uint size)
        {
            Debug.Assert(size <= maxEntities, "Component pool is greater than the max number of entities.");
            _components = new T[size];
            _entityMap = new int[maxEntities];
            Array.Fill(_entityMap, -1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(uint entityId, in T initialValue)
        {
            Debug.Assert(_entityMap[entityId] == -1, $"Component {typeof(T)} has already been added to entity {entityId}");
            _components[_entityMap[entityId] = ++_lastIndex] = initialValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(uint entityId)
        {
            Debug.Assert(_entityMap[entityId] == -1, $"Component {typeof(T)} has already been added to entity {entityId}");
            _entityMap[entityId] = ++_lastIndex;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(uint entityId)
        {
            Debug.Assert(_entityMap[entityId] != -1, $"Component {typeof(T)} does not exist on entity {entityId}");
            
            ref var index = ref _entityMap[entityId];
            if (index != _lastIndex)
            {
                for (var i = 0; i < _entityMap.Length; ++i)
                {
                    if (_entityMap[i] == _lastIndex)
                    {
                        _entityMap[i] = index;
                        _components[index] = _components[_lastIndex];
                        break;
                    }
                }
            }
            
            index = -1;
            --_lastIndex;
        }

        public ref T this[uint entityId]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _components[_entityMap[entityId]];
        }
    }
}
