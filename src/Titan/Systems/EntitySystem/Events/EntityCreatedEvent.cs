using Titan.Core.EventSystem;
using Titan.EntityComponentSystem.Entities;

namespace Titan.Systems.EntitySystem.Events
{
    internal readonly struct EntityCreatedEvent : IEvent
    {
        public Entity Entity { get; }
        public EventType Type => EventType.EntityCreated;
        public EntityCreatedEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}
