namespace Titan.EntityComponentSystem.Entities
{
    public interface IEntityManager
    {
        Entity Create();
        void Destroy(Entity entity);
    }
}
