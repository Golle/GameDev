using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Titan.ECS2
{
    internal class Publisher : IPublisher
    {
        private readonly ushort _worldId;
        private readonly IList<IDisposable> _subscriptions = new List<IDisposable>();
        public Publisher(ushort worldId)
        {
            _worldId = worldId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Publish<T>(in T message) where T : struct
        {
            Publisher<T>.Publish(_worldId, message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDisposable Subscribe<T>(MessageHandler<T> messageHandler) where T : struct
        {
            var subscription = Publisher<T>.Subscribe(_worldId, messageHandler);
            _subscriptions.Add(subscription);
            return subscription;
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }
    }
    internal static class Publisher<T> where T : struct
    {
        // TODO: add thread safety, locks for sub/unsub?

        private static MessageHandler<T>[] _messageHandlers = new MessageHandler<T>[1];
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Publish(ushort worldId, in T message)
        {
            if (_messageHandlers.Length > worldId)
            {
                _messageHandlers[worldId]?.Invoke(message);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Subscribe(ushort worldId, MessageHandler<T> messageHandler)
        {
            if (_messageHandlers.Length <= worldId)
            {
                Array.Resize(ref _messageHandlers, worldId + 1);
            }
            _messageHandlers[worldId] += messageHandler;
            return new Subscription(worldId, messageHandler);
        }
     
        private readonly struct Subscription : IDisposable
        {
            private readonly ushort _worldId;
            private readonly MessageHandler<T> _messageHandler;
            public Subscription(ushort worldId, MessageHandler<T> messageHandler)
            {
                _worldId = worldId;
                _messageHandler = messageHandler;
            }

            public void Dispose()
            {
                // ReSharper disable once DelegateSubtraction
                _messageHandlers[_worldId] -= _messageHandler;
            }
        }
    }
}
