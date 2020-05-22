namespace Titan.EntityComponentSystem.Components
{
    internal interface IComponentRegister
    {
        ulong Register<T>();
        ulong Get<T>();
    }
}
