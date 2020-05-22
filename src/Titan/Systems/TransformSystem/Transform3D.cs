using System.Numerics;
using Titan.EntityComponentSystem.Components;

namespace Titan.Systems.TransformSystem
{
    public class Transform3D
    {
        private bool _isDirty;
        private TransformData _transform;
        private int _numberOfParents;
        public ref readonly Vector3 Position => ref _transform.Position;
        public ref readonly Quaternion Rotation => ref _transform.Rotation;
        public ref readonly Vector3 Scale => ref _transform.Scale;
        public Transform3D Parent { get; private set; }
        public Matrix4x4 Transform => _transform.LocalTransform;

        public Transform3D()
        {
            Init();
        }

        public void SetParent(Transform3D parent)
        {
            _isDirty = true;
            Parent = parent;
        }

        public void SetPosition(in Vector3 position)
        {
            _transform.Position = position;
            _isDirty = true;
        }

        public void SetRotation(in Quaternion rotation)
        {
            _transform.Rotation = rotation;
            _isDirty = true;
        }

        public void SetScale(in Vector3 scale)
        {
            _transform.Scale= scale;
            _isDirty = true;
        }

        internal void Init()
        {
            _isDirty = true;
            _numberOfParents = 0;
            Parent = null;
            _transform.Init();
        }

        internal bool IsDirty() => _isDirty || (Parent?.IsDirty() ?? false);

        internal int UpdateParentCount()
        {
            _numberOfParents = Parent?.UpdateParentCount() ?? 0;
            return _numberOfParents;
        }

        internal void Update()
        {
            if (!IsDirty())
            {
                return;
            }
            _transform.Update(Parent?._transform.WorldTransform);
        }

        public void Reset()
        {
            //TODO: implement
        }
    }
}
