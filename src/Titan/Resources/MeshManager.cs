using System.Diagnostics;
using Titan.Components;
using Titan.Core.Logging;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics.Models;

namespace Titan.Resources
{
    internal class MeshManager : ResourceManager<string, IMesh>
    {
        private readonly IMeshLoader _meshLoader;
        private readonly ILogger _logger;

        public MeshManager(IMeshLoader meshLoader, ILogger logger)
        {
            _meshLoader = meshLoader;
            _logger = logger;
        }

        protected override IMesh Load(in string identifier)
        {
            var timer = Stopwatch.StartNew();
            var mesh = _meshLoader.Load(identifier);
            timer.Stop();
            _logger.Debug("Mesh: {0} loaded in {1} ms", identifier, timer.Elapsed.TotalMilliseconds);
            return mesh;
        }

        protected override void Unload(in string identifier, IMesh mesh)
        {
            _logger.Debug("Mesh: {0} unloaded", identifier);
            mesh.Dispose();
        }

        protected override void OnLoaded(Entity entity, in string identifier, IMesh mesh)
        {
            entity.AddComponent(new Model3D {Mesh = mesh});
        }
    }
}
