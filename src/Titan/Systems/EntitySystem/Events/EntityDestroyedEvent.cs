using Titan.Core.EventSystem;

namespace Titan.Systems.EntitySystem.Events
{
    internal readonly struct EntityDestroyedEvent : IEvent
    {
        public Entity Entity { get; }
        public EventType Type => EventType.EntityDestroyed;
        public EntityDestroyedEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}