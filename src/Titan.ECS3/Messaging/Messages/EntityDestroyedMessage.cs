using System.Runtime.InteropServices;

namespace Titan.ECS3.Messaging.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal readonly struct EntityDestroyedMessage
    {
        public uint Id { get; }
        public EntityDestroyedMessage(uint id)
        {
            Id = id;
        }
    }
}
