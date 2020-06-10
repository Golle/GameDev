namespace Titan.ECS2.Messages
{
    internal readonly struct EntityCreatedMessage
    {
        public uint Id { get; }
        public EntityCreatedMessage(uint id)
        {
            Id = id;
        }
    }
}
