using Titan.Core.EventSystem;
using Titan.EntityComponentSystem;
using Titan.EntityComponentSystem.Entities;

namespace Titan.Systems.EntitySystem.Events
{
    internal readonly struct EntityDisabledEvent : IEvent
    {
        public Entity Entity { get; }
        public EventType Type => EventType.EntityDisabled;

        public EntityDisabledEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}
