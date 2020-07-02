using System;
using Titan.Components;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Systems.Rendering
{
    internal class LightSystem : EntitySystem
    {
        private readonly Renderer3Dv2 _renderer;
        private readonly IComponentMap<Light> _light;
        private readonly IComponentMap<Transform3D> _transform;

        public LightSystem(IWorld world, Renderer3Dv2 renderer) 
            : base(world, world.EntityFilter().With<Light>().With<Transform3D>())
        {
            _renderer = renderer;
            _light = Map<Light>();
            _transform = Map<Transform3D>();
            // TODO: sort based on distance from the current camera
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var light = ref _light[entityId];
            ref var transform = ref _transform[entityId];



            _renderer.SubmitLight(light.Color, transform.Position);


        }
    }
}
