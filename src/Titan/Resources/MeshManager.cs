using System;
using System.Diagnostics;
using System.IO;
using Titan.Components;
using Titan.Core.Configuration;
using Titan.Core.Logging;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics.Models;

namespace Titan.Resources
{
    internal class MeshManager : ResourceManager<string, IMesh>
    {
        private readonly IMeshLoader _meshLoader;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public MeshManager(IMeshLoader meshLoader, IConfiguration configuration, ILogger logger)
        {
            _meshLoader = meshLoader;
            _configuration = configuration;
            _logger = logger;
        }

        protected override IMesh Load(in string identifier)
        {
            var filename = Path.Combine(_configuration.ResourcesPath, identifier);
            if (Path.GetExtension(filename) != ".obj")
            {
                throw new NotSupportedException($"File format {Path.GetExtension(filename)} is not supported.");
            }

            var timer = Stopwatch.StartNew();
            var mesh = _meshLoader.Load(filename);
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
