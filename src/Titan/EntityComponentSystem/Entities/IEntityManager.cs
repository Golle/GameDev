namespace Titan.EntityComponentSystem.Entities
{
    public interface IEntityManager
    {
        uint Create();
        void Free(uint entity);
    }
}
