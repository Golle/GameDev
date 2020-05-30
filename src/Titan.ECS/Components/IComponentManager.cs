using System;

namespace Titan.ECS.Components
{
    public interface IComponentManager
    {
        void RegisterComponent<T>(uint capacity) where T : unmanaged;
        void RegisterComponent(Type type, uint capacity);
        IComponentMapper<T> GetComponentMapper<T>() where T : unmanaged;
        ulong Create<T>(uint entity) where T : unmanaged;
        ulong Create<T>(uint entity, in T initialData) where T : unmanaged;
        void Free(uint entityId, ulong componentId);
    }
}
