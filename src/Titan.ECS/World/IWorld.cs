namespace Titan.ECS.World
{
    public interface IWorld
    {
        IEntity CreateEntity();
        void Destroy();
    }
}
