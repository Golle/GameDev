using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Numerics;
using Titan.Components;
using Titan.Core.Configuration;
using Titan.Core.Logging;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics.Models;
using Titan.Graphics.Textures;

namespace Titan.Resources
{
    internal class MeshGroupManager : ResourceManager<string, MeshGroup>
    {
        private readonly IMeshLoader _meshLoader;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public MeshGroupManager(IMeshLoader meshLoader, IConfiguration configuration, ILogger logger)
        {
            _meshLoader = meshLoader;
            _configuration = configuration;
            _logger = logger;
        }

        protected override MeshGroup Load(in string identifier)
        {
            var filename = Path.Combine(_configuration.Paths.Models, identifier);
            if (Path.GetExtension(filename) != ".dat")
            {
                throw new NotSupportedException($"File format {Path.GetExtension(filename)} is not supported.");
            }

            var timer = Stopwatch.StartNew();
            var meshes = _meshLoader.Load(filename);
            timer.Stop();
            _logger.Debug("Mesh: {0} loaded in {1} ms", identifier, timer.Elapsed.TotalMilliseconds);
            return new MeshGroup{Meshes = meshes};
        }

        protected override void OnLoaded(Entity entity, in string identifier, MeshGroup group)
        {
            entity.AddComponent(new Model3D { Mesh = group.Meshes[0] });

            for (var i = 1; i < group.Meshes.Length; ++i)
            {
                var child = entity.CreateChild();
                child.AddComponent(new Transform3D{Scale = Vector3.One});
                child.AddComponent(new Model3D{Mesh = group.Meshes[i]});
                child.AddComponent(new Resource<string, ITexture2D>(@"spnza_bricks_a_diff.png")); // temp
            }
        }

        protected override void Unload(in string identifier, MeshGroup group)
        {
            _logger.Debug("Mesh: {0} unloaded", identifier);
            group.Dispose();
        }
    }

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
            var filename = Path.Combine(_configuration.Paths.Models, identifier);
            if (Path.GetExtension(filename) != ".dat")
            {
                throw new NotSupportedException($"File format {Path.GetExtension(filename)} is not supported.");
            }

            var timer = Stopwatch.StartNew();
            var meshes = _meshLoader.Load(filename);
            timer.Stop();
            _logger.Debug("Mesh: {0} loaded in {1} ms", identifier, timer.Elapsed.TotalMilliseconds);
            return meshes[0];
        }

        protected override void OnLoaded(Entity entity, in string identifier, IMesh meshes)
        {
            entity.AddComponent(new Model3D { Mesh = meshes });
        }

        protected override void Unload(in string identifier, IMesh mesh)
        {
            _logger.Debug("Mesh: {0} unloaded", identifier);
            mesh.Dispose();
        }
    }
}
