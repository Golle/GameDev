using System.Runtime.InteropServices;
using Titan.Core.EventSystem;

namespace Titan.ECS.Entities.Events
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal readonly struct EntityDisabledEvent : IEvent
    {
        public uint Entity { get; }
        public EventType Type => EventType.EntityDisabled;
        public EntityDisabledEvent(uint entity)
        {
            Entity = entity;
        }
    }
}
