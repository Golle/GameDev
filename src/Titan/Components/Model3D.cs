using System.Runtime.InteropServices;
using Titan.Graphics.Models;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Model3D
    {
        public IMesh Mesh;
    }
}
