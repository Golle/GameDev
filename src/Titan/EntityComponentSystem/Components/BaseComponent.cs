using Titan.EntityComponentSystem.Configuration;
using Titan.Systems.Components;

namespace Titan.EntityComponentSystem.Components
{
    public abstract class BaseComponent : IComponent
    {
        public ulong Id { get; }
        protected BaseComponent()
        {
            Id = this.Mask();
        }
        public abstract void Reset();
    }
}