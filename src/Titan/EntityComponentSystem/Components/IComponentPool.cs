using Titan.Systems.Components;

namespace Titan.EntityComponentSystem.Components
{
    public interface IComponentPool<T> where T : IComponent
    {
        T Get();
        void Put(T component);
    }
}
