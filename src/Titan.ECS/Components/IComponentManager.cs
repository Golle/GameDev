using System;

namespace Titan.ECS.Components
{
    public interface IComponentManager
    {
        IComponentManager RegisterComponent<T>(uint capacity) where T : struct;
        IComponentManager RegisterComponent(Type type, uint capacity);
        IComponentMapper<T> GetComponentMapper<T>() where T : struct;
        ulong Create<T>(uint entity) where T : struct;
        ulong Create<T>(uint entity, in T initialData) where T : struct;
        void Free(uint entityId, ulong componentId);
    }
}
