namespace Titan.ECS3.Messaging.Messages
{
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