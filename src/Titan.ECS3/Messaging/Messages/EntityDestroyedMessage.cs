namespace Titan.ECS3.Messaging.Messages
{
    internal readonly struct EntityDestroyedMessage
    {
        public uint Id { get; }
        public EntityDestroyedMessage(uint id)
        {
            Id = id;
        }
    }
}