namespace Titan.ECS2.Messages
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
