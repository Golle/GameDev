namespace Titan.ECS.Messaging.Messages
{
    internal readonly struct WorldDisposedMessage
    {
        public uint Id { get; }
        public WorldDisposedMessage(uint id)
        {
            Id = id;
        }
    }
}
