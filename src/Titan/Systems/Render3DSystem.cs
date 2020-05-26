using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Titan.Components;
using Titan.D3D11;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Systems
{
    internal class Render3DSystem : BaseSystem
    {
        private readonly IRenderer _renderer;
        private readonly IComponentMapper<Transform3D> _transform;
        private IComponentMapper<Mesh> _model;
        private IComponentMapper<Material> _material;
        
        public Render3DSystem(IComponentManager componentManager, IRenderer renderer)
        : base(typeof(Transform3D), typeof(Mesh), typeof(Material))
        {
            _renderer = renderer;
            _transform = componentManager.GetComponentMapper<Transform3D>();
            _model = componentManager.GetComponentMapper<Mesh>();
            _material = componentManager.GetComponentMapper<Material>();
        }
        protected override void Update(float deltaTime, uint entity)
        {

            var model = GetModel(entity);
            for (var i = 0; i < model.Vertices.Length; ++i)
            {
                
                model.Vertices[i].Position += _transform[entity].Position;
            }
            _renderer.Push(model);
        }


        private RendereableModel GetModel(uint entity)
        {
            Vertex[] vertices;
            if (entity % 2== 0)
            {
                vertices = new[]
                {
                    new Vertex {Color = new Color(1f, 0, 0), Position = new Vector3(-2f, -2f, -2f)},
                    new Vertex {Color = new Color(1f, 1f, 0), Position = new Vector3(2f, -2f, -2f)},
                    new Vertex {Color = new Color(1f, 0, 1f), Position = new Vector3(-2f, 2f, -2f)},
                    new Vertex {Color = new Color(0f, 1f, 0), Position = new Vector3(2f, 2f, -2f)},
                    new Vertex {Color = new Color(0f, 1f, 1f), Position = new Vector3(-2f, -2f, 2f)},
                    new Vertex {Color = new Color(1f, 1f, 0), Position = new Vector3(2f, -2f, 2f)},
                    new Vertex {Color = new Color(1f, 1f, 1f), Position = new Vector3(-2f, 2f, 2f)},
                    new Vertex {Color = new Color(1f, 0, 0), Position = new Vector3(2f, 2f, 2f)},
                };
            }
            else
            {
                vertices = new[]
                {
                    new Vertex {Color = new Color(1f, 0, 0), Position = new Vector3(-1f, -1f, -1f)},
                    new Vertex {Color = new Color(1f, 1f, 0), Position = new Vector3(1f, -1f, -1f)},
                    new Vertex {Color = new Color(1f, 0, 1f), Position = new Vector3(-1f, 1f, -1f)},
                    new Vertex {Color = new Color(0f, 1f, 0), Position = new Vector3(1f, 1f, -1f)},
                    new Vertex {Color = new Color(0f, 1f, 1f), Position = new Vector3(-1f, -1f, 1f)},
                    new Vertex {Color = new Color(1f, 1f, 0), Position = new Vector3(1f, -1f, 1f)},
                    new Vertex {Color = new Color(1f, 1f, 1f), Position = new Vector3(-1f, 1f, 1f)},
                    new Vertex {Color = new Color(1f, 0, 0), Position = new Vector3(1f, 1f, 1f)},
                };
            }

            var indices = new short[]
            {
                0,2,1,  2,3,1,
                1,3,5,  3,7,5,
                2,6,3,  3,6,7,
                4,5,7,  4,7,6,
                0,4,2,  2,4,6,
                0,1,4,  1,5,4
            };
            return new RendereableModel(vertices, indices, Vector3.Zero);
        }


        
    }
}
