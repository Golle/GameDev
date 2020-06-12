namespace Titan.ECS.Entities
{
    public interface IEntityManager
    {
        uint Create();
        void Free(uint entity);


    }
}
