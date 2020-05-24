namespace Titan.ECS.Systems
{
    internal interface ISystemsRunner
    {
        void RegisterSystem(ISystem system);
        void Update(float deltaTime);
    }
}
