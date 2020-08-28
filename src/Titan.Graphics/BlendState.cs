using System;
using Titan.D3D11;
using Titan.D3D11.Device;

namespace Titan.Graphics
{
    internal class BlendState : IBlendState
    {
        private readonly ID3D11BlendState _blendState;
        private readonly Color _blendFactor;

        public ref readonly Color BlendFactor => ref _blendFactor;
        public uint SampleMask { get; }
        public IntPtr NativeHandle => _blendState.Handle;

        public BlendState(ID3D11BlendState blendState)
        {
            _blendState = blendState;
            _blendFactor = new Color(1f, 1f, 1f, 1f);
            SampleMask = 0xffffffff;;
        }

        public void Dispose()
        {
            _blendState.Dispose();
        }
    }
}
