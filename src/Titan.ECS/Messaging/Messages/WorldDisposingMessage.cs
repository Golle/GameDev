namespace Titan.ECS.Messaging.Messages
{
    internal readonly struct WorldDisposingMessage
    {
        public uint Id { get; }
        public WorldDisposingMessage(uint id)
        {
            Id = id;
        }
    }
}
