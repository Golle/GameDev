using System.Collections.Generic;

namespace Titan.Core.EventSystem
{
    internal class EventHandler<T> : IEventHandler where T : struct, IEvent
    {
        private readonly Queue<T> _eventQueue = new Queue<T>();
        private readonly IList<IEventManager.EventCallback<T>> _listeners = new List<IEventManager.EventCallback<T>>();
        public void Subscribe(IEventManager.EventCallback<T> callback)
        {
            _listeners.Add(callback);
        }
        public void Unsubscribe(IEventManager.EventCallback<T> callback)
        {
            _listeners.Remove(callback);
        }

        public void PublishImmediate(in T @event)
        {
            for (var i = 0; i < _listeners.Count; ++i)
            {
                _listeners[i](@event);
            }
        }
        
        public void Publish(in T @event)
        {
            _eventQueue.Enqueue(@event);
        }

        public void Update()
        {
            while(_eventQueue.TryDequeue(out var @event))
            {
                for (var i = 0; i < _listeners.Count; ++i)
                {
                    _listeners[i](@event);
                }
            }
        }
    }
}
