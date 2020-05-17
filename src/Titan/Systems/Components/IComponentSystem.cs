namespace Titan.Systems.Components
{
    internal interface IComponentSystem
    {
        IComponent Create(ComponentId id);
    }
}
