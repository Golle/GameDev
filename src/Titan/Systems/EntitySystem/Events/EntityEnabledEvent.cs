using Titan.Core.EventSystem;

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