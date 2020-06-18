namespace Titan.ECS3.Systems
{
    public interface ISystem
    {
        void PreUpdate();
        void Update(float deltaTime);
    }
}
