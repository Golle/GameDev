namespace Titan.ECS.Components
{
    internal interface IComponentPool
    {
        void Free(uint index);
        uint Create();
    }

    internal interface IComponentPool<T> : IComponentPool where T : struct
    {
        uint Create(out T value);
        ref T Get(uint index);
        ref T this[uint index] { get; }
    }
}
