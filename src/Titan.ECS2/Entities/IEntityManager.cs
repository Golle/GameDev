namespace Titan.ECS2.Entities
{
    internal interface IEntityManager
    {
        Entity Create();
        void Destroy(uint entityId);

        ref EntityInfo GetEntityInfo(uint entityId);
    }
}
