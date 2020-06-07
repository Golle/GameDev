using System.Runtime.InteropServices;
using Titan.Core.EventSystem;

namespace Titan.ECS.Entities.Events
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct EntityCreatedEvent : IEvent
    {
        public uint Entity { get; }
        public EventType Type => EventType.EntityCreated;
        public EntityCreatedEvent(uint entity)
        {
            Entity = entity;
        }
    }
}
