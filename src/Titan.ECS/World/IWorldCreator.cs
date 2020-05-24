namespace Titan.ECS.World
{
    public interface IWorldCreator
    {
        IWorld CreateWorld(WorldConfiguration configuration);
    }
}
