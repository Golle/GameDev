using System;
using System.Diagnostics;
using Titan.Components;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics.Models;

namespace Titan.Resources
{
    internal class MeshManager : ResourceManager<string, IMesh>
    {
        private readonly IMeshLoader _meshLoader;
        public MeshManager(IMeshLoader meshLoader)
        {
            _meshLoader = meshLoader;
        }

        protected override IMesh Load(in string identifier)
        {
            var timer = Stopwatch.StartNew();
            var mesh = _meshLoader.Load(identifier);
            timer.Stop();
            Console.WriteLine($"Mesh: {identifier} loaded in {timer.Elapsed.TotalMilliseconds} ms");
            return mesh;
        }

        protected override void Unload(in string identifier, IMesh mesh)
        {
            Console.WriteLine($"Mesh {identifier} unloaded");
            mesh.Dispose();
        }

        protected override void OnLoaded(Entity entity, in string identifier, IMesh mesh)
        {
            entity.AddComponent(new Model3D {Mesh = mesh});
        }
    }
}
