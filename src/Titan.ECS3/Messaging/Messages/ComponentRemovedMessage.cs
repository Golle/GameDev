using System.Runtime.InteropServices;
using Titan.ECS3.Components;

namespace Titan.ECS3.Messaging.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal readonly struct ComponentRemovedMessage<T>
    {
        public uint EntityId { get; }
        public ComponentMask Components { get; }

        public ComponentRemovedMessage(uint entityId, in ComponentMask components)
        {
            EntityId = entityId;
            Components = components;
        }
    }
}
