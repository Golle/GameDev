using Titan.ECS2.Entities;

namespace Titan.ECS2
{
    public interface IWorld
    {
        Entity CreateEntity();
        void Destroy();
    }
}
