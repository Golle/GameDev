using System;

namespace Titan.ECS2.Components
{
    internal interface IComponentManager : IDisposable
    {
        void RegisterComponent<T>(uint size) where T : struct;
        void RegisterComponent(Type type, uint size);

        IComponentMapper<T> GetMapper<T>() where T : struct;
    }
}
