namespace Titan.EntityComponentSystem.Entities
{
    public interface IEntityManager
    {
        Entity Create();
        void Free(Entity entity);
    }
}
