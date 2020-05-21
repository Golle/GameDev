using Titan.Core.EventSystem;
using Titan.EntityComponentSystem;
using Titan.EntityComponentSystem.Entities;

namespace Titan.Systems.EntitySystem.Events
{
    internal readonly struct EntityEnabledEvent : IEvent
    {
        public Entity Entity { get; }
        public EventType Type => EventType.EntityDestroyed;
        public EntityEnabledEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}
