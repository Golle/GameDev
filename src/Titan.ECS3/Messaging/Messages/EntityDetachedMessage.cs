namespace Titan.ECS3.Messaging.Messages
{
    internal readonly struct EntityDetachedMessage
    {
        public uint ParentId { get; }
        public uint ChildId { get; }

        public EntityDetachedMessage(uint parentId, uint childId)
        {
            ParentId = parentId;
            ChildId = childId;
        }
    }
}