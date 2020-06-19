using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Titan.ECS3.Messaging
{
    internal class Publisher : IDisposable
    {
        private readonly uint _worldId;
        private readonly IList<IDisposable> _subscriptions = new List<IDisposable>();
        
        public Publisher(uint worldId)
        {
            _worldId = worldId;
        }

        internal IDisposable Subscribe<T>(MessageHandler<T> messageHandler)
        {
            var disposable = PublisherInternal<T>.Subscribe(_worldId, messageHandler);
            _subscriptions.Add(disposable);
            return disposable;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Publish<T>(in T message)
        {
            Console.WriteLine($"Message: {message.GetType().Name}");
            PublisherInternal<T>.Publish(_worldId, message);
        }

        private readonly struct Subscription<T> : IDisposable
        {
            private readonly uint _worldId;
            private readonly MessageHandler<T> _messageHandler;
            internal Subscription(uint worldId, MessageHandler<T> messageHandler)
            {
                _worldId = worldId;
                _messageHandler = messageHandler;
            }

            public void Dispose()
            {
                PublisherInternal<T>.Unsubscribe(_worldId, _messageHandler);
            }
        }

        private static class PublisherInternal<T>
        {
            private static readonly MessageHandler<T>[] MessageHandlers = new MessageHandler<T>[10];
            internal static IDisposable Subscribe(uint worldId, MessageHandler<T> messageHandler)
            {
                MessageHandlers[worldId] += messageHandler;
                return new Subscription<T>(worldId, messageHandler);
            }

            public static void Unsubscribe(uint worldId, MessageHandler<T> messageHandler)
            {
                MessageHandlers[worldId] -= messageHandler;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Publish(uint worldId, in T message) => MessageHandlers[worldId]?.Invoke(message);
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose(); 
            }
        }
    }
}
