namespace Titan.Core.EventSystem
{
    public interface IEventManager
    {
        public delegate void EventCallback<T>(in T @event) where T: struct, IEvent;
        void Subscribe<T>(EventCallback<T> callback) where T : struct, IEvent;
        void Unsubscribe<T>(EventCallback<T> callback) where T : struct, IEvent; // ?
        void Publish<T>(in T @event) where T : struct, IEvent;
        void PublishImmediate<T>(in T @event) where T : struct, IEvent;
        void PublishDelayed<T>(in T @event, uint ticks) where T : struct, IEvent;
        void Update();
    }
}
