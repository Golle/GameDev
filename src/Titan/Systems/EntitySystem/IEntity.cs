using Titan.Systems.Components;

namespace Titan.Systems.EntitySystem
{
    internal interface IEntity
    {
        T AddComponent<T>(ComponentId id) where T : IComponent;
        T GetComponent<T>(ComponentId id) where T : IComponent;
        void SetActive(bool active);
        void Destroy();

    }
}
