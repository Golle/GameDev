using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Titan.Core.EventSystem;
using Titan.ECS.Components;

namespace Titan.ECS.Systems
{
    internal class ComponentMapper<T> : IComponentMapper<T> where T : struct
    {
        private readonly IEventManager _eventManager;

        private readonly IComponentPool<T> _pool;
        private readonly IDictionary<uint, uint> _componentIndexMap;
        private ulong _id;

        public ComponentMapper(uint poolSize, IEventManager eventManager)
        {
            _eventManager = eventManager;
            _pool = new ComponentPool<T>(poolSize);
            _componentIndexMap = new Dictionary<uint, uint>();
            _id = typeof(T).ComponentMask();
        }

        public void DestroyComponent(uint entity)
        {
            _componentIndexMap.Remove(entity, out var index);
            _pool.Free(index);

            _eventManager.Publish(new ComponentDestroyedEvent(entity, _id));
        }

        public ref T CreateComponent(uint entity)
        {
            var index = _pool.Create();
            _componentIndexMap[entity] = index;

            _eventManager.Publish(new ComponentCreatedEvent(entity, _id));
            return ref _pool[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get(uint entity) => ref _pool[_componentIndexMap[entity]];

        public ref T this[uint entity] => ref _pool[_componentIndexMap[entity]];
    }

    internal readonly struct ComponentDestroyedEvent : IEvent
    {
        public ulong Id { get; }
        public uint EntityId { get; }
        public EventType Type => EventType.ComponentDestroyed;
        public ComponentDestroyedEvent(uint entityId, ulong id)
        {
            EntityId = entityId;
            Id = id;
        }
    }
    internal readonly struct ComponentCreatedEvent : IEvent
    {
        public uint EntityId { get; }
        public ulong Id { get; }
        public EventType Type => EventType.ComponentCreated;
        public ComponentCreatedEvent(uint entityId, ulong id)
        {
            EntityId = entityId;
            Id = id;
        }
    }
}
