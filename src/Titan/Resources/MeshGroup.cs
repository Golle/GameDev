using System;
using Titan.Graphics.Models;

namespace Titan.Resources
{
    public class MeshGroup : IDisposable
    {
        public IMesh [] Meshes { get; set; }
        public void Dispose()
        {
            foreach (var mesh in Meshes)
            {
                mesh.Dispose();
            }
        }
    }
}
