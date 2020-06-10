using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Titan.ECS2
{
    internal class SimplePublisher : IPublisher
    {
        private readonly ushort _worldId;
        private readonly IList<IDisposable> _subscriptions = new List<IDisposable>();
        public SimplePublisher(ushort worldId)
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
}
