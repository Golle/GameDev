using System.Runtime.InteropServices;
using Titan.Core.EventSystem;

namespace Titan.ECS.Entities.Events
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal readonly struct EntityEnabledEvent : IEvent
    {
        public uint Entity { get; }
        public EventType Type => EventType.EntityEnabled;
        public EntityEnabledEvent(uint entity)
        {
            Entity = entity;
        }
    }
}
