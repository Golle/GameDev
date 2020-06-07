namespace Titan.ECS.World
{
    public interface IWorld
    {
        uint CreateEntity();

        void AddComponent<T>(uint entity) where T : struct;
        void AddComponent<T>(uint entity, in T initialData) where T : struct;

        ref T GetComponent<T>(uint entity) where T : struct;
        void RemoveComponent<T>(uint entity) where T : struct;


        void Destroy();
        void SetParent(uint entity, uint parentEntity);
    }
}
