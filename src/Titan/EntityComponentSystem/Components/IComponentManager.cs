using Titan.Systems.Components;

namespace Titan.EntityComponentSystem.Components
{
    public interface IComponentManager
    {
        void Register<T>(IComponentPool<T> componentPool) where T : IComponent;
        T Create<T>() where T : IComponent;
        void Destroy<T>(T component) where T : IComponent;
    }
}
