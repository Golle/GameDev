namespace Titan.Systems.EntitySystem
{
    internal interface IEntityManager
    {
        IEntity Create(string? name = null);
    }
}
