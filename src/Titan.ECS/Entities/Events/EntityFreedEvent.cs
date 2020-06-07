using System.Runtime.InteropServices;
using Titan.Core.EventSystem;

namespace Titan.ECS.Entities.Events
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal readonly struct EntityFreedEvent : IEvent
    {
        public uint Entity { get; }
        public EventType Type => EventType.EntityFreed;
        public EntityFreedEvent(uint entity)
        {
            Entity = entity;
        }
    }
}
