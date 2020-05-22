namespace Titan.Systems.Components
{
    public interface IComponent
    {
        ulong Id { get; }
        void Reset();
    }
}
