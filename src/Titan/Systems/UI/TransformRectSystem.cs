using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Titan.Components;
using Titan.Core.Math;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Windows.Window;

namespace Titan.Systems.UI
{
    internal class TransformRectSystem : EntitySystem
    {
        private readonly IWindow _window;
        private readonly IComponentMap<TransformRect> _transform;
        private readonly IRelationship _relationship;
        private BoundingBox2D _windowBoundingBox;

        public TransformRectSystem(IWorld world, IWindow window) : base(world, world.EntityFilter().With<TransformRect>())
        {
            _window = window;
            _transform = Map<TransformRect>();
            _relationship = Relationship();
        }

        protected override void OnPreUpdate()
        {
            _windowBoundingBox = new BoundingBox2D(Vector2.Zero, new Size(_window.Width, _window.Height));// use the window size if there's not parent
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var transform = ref _transform[entityId];
            ref var worldPosition = ref transform.WorldPosition;

            if (_relationship.TryGetParent(entityId, out var parentId) && _transform.Has(parentId))
            {
                ref var transformParent = ref _transform[parentId];
                worldPosition = CalculateWorldPosition(transform, transformParent.BoundingBox);
            }
            else
            {
                worldPosition = CalculateWorldPosition(transform, _windowBoundingBox);
            }

            
            transform.BoundingBox = new BoundingBox2D(new Vector2(worldPosition.X, worldPosition.Y), transform.Size);
        }

        private static Vector3 CalculateWorldPosition(in TransformRect transform, in BoundingBox2D box)
        {
            var max = box.Max;
            var min = box.Min;
            var center = box.Center;
            var size = transform.Size;
            var offset = transform.Offset;

            switch (transform.AnchorPoint)
            {
                case AnchorPoint.TopRight:
                    return new Vector3(max.X + offset.X - size.Width, max.Y + offset.Y - size.Height, offset.Z);
                case AnchorPoint.Right:
                    return new Vector3(max.X + offset.X - size.Width, center.Y + offset.Y - size.Height / 2f, offset.Z);
                case AnchorPoint.BottomRight:
                    return new Vector3(max.X + offset.X - size.Width, min.Y + offset.Y, offset.Z);
                case AnchorPoint.Bottom:
                    return new Vector3(center.X + offset.X - size.Width / 2f, min.Y + offset.Y, offset.Z);
                case AnchorPoint.Center:
                    return new Vector3(center.X + offset.X - size.Width / 2f, center.Y + offset.Y - size.Height / 2f, offset.Z);
                case AnchorPoint.Top:
                    return new Vector3(center.X + offset.X - size.Width / 2f, max.Y + offset.Y - size.Height, offset.Z);
                case AnchorPoint.Left:
                    return new Vector3(min.X + offset.X, center.Y + offset.Y -size.Height/2f, offset.Z);
                case AnchorPoint.TopLeft:
                    return new Vector3(min.X + offset.X, max.Y + offset.Y - size.Height, offset.Z);
                case AnchorPoint.BottomLeft:
                    return new Vector3(min.X + offset.X, min.Y + offset.Y, offset.Z);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
