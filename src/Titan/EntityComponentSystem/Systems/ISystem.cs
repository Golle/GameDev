namespace Titan.EntityComponentSystem.Systems
{
    public interface ISystem
    {
        ulong Signature { get; }
        void Remove(uint entity);
        void Add(uint entity);
        bool IsMatch(ulong signature);
    }
}
