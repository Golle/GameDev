using System.Runtime.InteropServices;
using Titan.Core.EventSystem;

namespace Titan.ECS.Entities.Events
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal readonly struct EntityDestroyedEvent : IEvent
    {
        public uint Entity { get; }
        public EventType Type => EventType.EntityDestroyed;
        public EntityDestroyedEvent(uint entity)
        {
            Entity = entity;
        }
    }
}
