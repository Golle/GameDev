using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Core.Assets
{
    public interface IModel3D
    {
        Vector3[] Vertices { get; }
        Vector2[] Texture { get; }
        short[] Indices { get; }
    }
}
