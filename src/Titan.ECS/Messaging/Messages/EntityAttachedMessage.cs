using System.Runtime.InteropServices;

namespace Titan.ECS.Messaging.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal readonly struct EntityAttachedMessage
    {
        public uint ParentId { get; }
        public uint ChildId { get; }
        public EntityAttachedMessage(uint parentId, uint childId)
        {
            ParentId = parentId;
            ChildId = childId;
        }
    }
}
