namespace Titan.ECS
{
    public interface IRelationship
    {
        bool TryGetParent(uint entityId, out uint parentId);
    }
}
