using System;
using Titan.D3D11;

namespace Titan.Graphics
{
    public interface IBlendState : IDisposable
    {
        ref readonly Color BlendFactor { get; }
        uint SampleMask { get; }
        IntPtr NativeHandle { get; }
    }
}
