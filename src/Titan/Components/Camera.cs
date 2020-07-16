using System.Numerics;
using System.Runtime.InteropServices;
using Titan.Core.Math;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Components
{

    public enum CameraProjection : byte
    {
        Perspective,
        Orthographic
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Camera
    {
        public float Width;
        public float Height;
        public float FieldOfView;
        public float NearPlane;
        public float FarPlane;

        public float Pitch;
        public float Yaw;
        public float Roll;

        public Vector3 Up;
        public Vector3 Forward;

        public CameraProjection Projection;

        internal Matrix4x4 ViewMatrix;
        internal Matrix4x4 ProjectionMatrix;
        internal Matrix4x4 ViewProjectionMatrix;
    }


    internal class Camera3DSystem : EntitySystem
    {
        private readonly Renderer3Dv2 _renderer;
        private readonly IComponentMap<Transform3D> _transform;
        private readonly IComponentMap<Camera> _camera;
        private readonly IRelationship _relationShip;

        private static readonly Vector3 DefaultForward = new Vector3(0,0,1);
        private static readonly Vector3 DefaultRight = new Vector3(1,0,0);
        private static readonly Vector3 DefaultUp = new Vector3(0,1,0);
        public Camera3DSystem(IWorld world, Renderer3Dv2 renderer) 
            : base(world, world.EntityFilter().With<Camera>().With<Transform3D>())
        {
            _renderer = renderer;
            _camera = Map<Camera>();
            _transform = Map<Transform3D>();
            _relationShip = Relationship();
        }
        
        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            // TODO: this needs optimzations. 
            ref var camera = ref _camera[entityId];
            ref var transform = ref _transform[entityId];
            
            camera.ProjectionMatrix = MatrixExtensions.CreatePerspectiveLH(camera.Width, camera.Height, camera.NearPlane, camera.FarPlane);

            //var rotationMatrix = Matrix4x4.CreateFromYawPitchRoll(camera.Yaw, camera.Pitch, camera.Roll);
            camera.Yaw += 0.3f * deltaTime;
            //var rotationQuat = Quaternion.CreateFromYawPitchRoll(camera.Yaw, camera.Pitch, camera.Roll);

            var position = transform.Position;
            var rotation = transform.Rotation; // TODO: maybe use this?
            if (_relationShip.TryGetParent(entityId, out var parentId) && _transform.Has(parentId))
            {
                ref var parentTransform = ref _transform[parentId];
                position = Vector3.Transform(position, parentTransform.WorldTransform); 
                //rotation = Quaternion.Add(rotation, parentTransform.Rotation); // TODO: maybe use this?
                rotation = parentTransform.Rotation;
                //rotation = parentTransform.Rotation;
             }

            //rotation = rotationQuat;

            camera.Forward = Vector3.Transform(DefaultForward, rotation);
            camera.Up = Vector3.Transform(DefaultUp, rotation);
            

            camera.ViewMatrix = Matrix4x4.CreateLookAt(position, position + camera.Forward, camera.Up);

            camera.ViewProjectionMatrix =
                //Matrix4x4.CreateTranslation(position) *
                //transform.WorldTransform *
                //Matrix4x4.CreateFromYawPitchRoll(rotation.X, rotation.Y, rotation.Z) *
                //Matrix4x4.CreateFromQuaternion(rotation) * 
                camera.ViewMatrix *
                camera.ProjectionMatrix;
            _renderer.SetCamera(camera.ViewProjectionMatrix, camera.ViewMatrix);
        }
    }
}
