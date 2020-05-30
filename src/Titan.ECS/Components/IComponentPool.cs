namespace Titan.ECS.Components
{
    internal interface IComponentPool
    {
        void Free(uint index);
        uint Create();
    }

    internal interface IComponentPool<T> : IComponentPool where T : unmanaged
    {
        uint Create(out T value);
        ref T Get(uint index);
        ref T this[uint index] { get; }
    }
}
