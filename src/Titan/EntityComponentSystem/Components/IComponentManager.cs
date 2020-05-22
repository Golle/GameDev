using Titan.EntityComponentSystem.Entities;

namespace Titan.EntityComponentSystem.Components
{
    internal interface IComponentManager
    {
        void RegisterPool<T>(IComponentPool<T> pool) where T : unmanaged; 
        Component Create<T>(Entity entity) where T : unmanaged;
        void Free(in Component component);
    }
}
