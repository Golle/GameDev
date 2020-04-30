namespace Titan.Core.EventSystem
{
    public class EventManager : IEventManager
    {
        private readonly IEventHandler[] _eventHandler = new IEventHandler[(int)EventType.NumberOfEvents];
        public void Subscribe<T>(IEventManager.EventCallback<T> callback) where T : struct, IEvent
        {
            var eventType = (int)default(T).Type;
            ((EventHandler<T>)(_eventHandler[eventType] ?? (_eventHandler[eventType] = new EventHandler<T>()))).Subscribe(callback);
        }

        public void Unsubscribe<T>(IEventManager.EventCallback<T> callback) where T : struct, IEvent
        {
            var eventType = (int)default(T).Type;
            ((EventHandler<T>)_eventHandler[eventType])?.Unsubscribe(callback);
        }

        public void Publish<T>(in T @event) where T : struct, IEvent
        {
            ((EventHandler<T>)_eventHandler[(int) @event.Type])?.Publish(@event);
        }

        public void PublishImmediate<T>(in T @event) where T : struct, IEvent
        {
            ((EventHandler<T>)_eventHandler[(int) @event.Type])?.PublishImmediate(@event);
        }

        public void PublishDelayed<T>(in T @event, uint ticks) where T : struct, IEvent
        {
            throw new System.NotImplementedException("Delayed events are not support yet. Need a game loop with ticks");
        }

        public void Update()
        {
            for (var i = 0; i < (int) EventType.NumberOfEvents; ++i)
            {
                _eventHandler[i]?.Update();
            }
            
        }
    }
}
