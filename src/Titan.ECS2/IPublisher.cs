using System;

namespace Titan.ECS2
{
    internal interface IPublisher : IDisposable
    {
        void Publish<T>(in T message) where T : struct;
        IDisposable Subscribe<T>(MessageHandler<T> messageHandler) where T : struct;
    }


    internal readonly struct WorldDestroyedMessage
    {
        public ushort Id { get; }
        public WorldDestroyedMessage(ushort id)
        {
            Id = id;
        }
    }
}
