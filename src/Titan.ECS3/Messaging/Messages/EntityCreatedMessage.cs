namespace Titan.ECS3.Messaging.Messages
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
