namespace Titan.ECS.Systems
{
    public interface ISystem
    {
        ulong Signature { get; }
        void Remove(uint entity);
        void Add(uint entity);

        void Update(float deltaTime);
        bool IsMatch(ulong signature);
        bool Contains(ulong componentId);
    }
}
