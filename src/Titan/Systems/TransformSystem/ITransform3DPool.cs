namespace Titan.Systems.TransformSystem
{
    internal interface ITransform3DPool
    {
        Transform3D GetOrCreate();
        void Return(Transform3D transform);
    }
}
