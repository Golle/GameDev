using Titan.Core.EventSystem;

namespace Titan.Systems.Components.Events
{
    internal interface IComponentEvent : IEvent
    {
        IComponent Component { get; }
    }
}
