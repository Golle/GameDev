namespace Titan.ECS.Components
{
    internal interface IComponentPool
    {
        void Free(uint index);
        uint Create();
    }
    internal interface IComponentPool<T> : IComponentPool where T : unmanaged
    {
        ref T Create(out uint index);
        ref T Get(uint index);
        ref T this[uint index] { get; }
        
    }
}
