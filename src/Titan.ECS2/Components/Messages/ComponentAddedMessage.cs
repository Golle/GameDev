using System.Runtime.InteropServices;

namespace Titan.ECS2.Components.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal readonly struct ComponentAddedMessage<T>
    {
        public uint EntityId { get; }
        public ComponentSignature Components { get; }
        public ComponentAddedMessage(uint entityId, in ComponentSignature components)
        {
            EntityId = entityId;
            Components = components;
        }
    }
}