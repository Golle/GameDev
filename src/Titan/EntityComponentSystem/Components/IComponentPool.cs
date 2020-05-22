namespace Titan.EntityComponentSystem.Components
{
    internal interface IComponentPool
    {
        void Free(uint index);
    }
    internal interface IComponentPool<T> : IComponentPool where T : unmanaged
    {
        ref T Create(out uint index);
        uint Create();
        ref T Get(uint index);
        ref T this[uint index] { get; }
        
    }
}
