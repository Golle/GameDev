using System.Runtime.InteropServices;

namespace Titan.ECS3.Messaging.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal readonly struct EntityCreatedMessage
    {
        public uint Id { get; }
        public EntityCreatedMessage(uint id)
        {
            Id = id;
        }
    }
}
