using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Textures
{
    internal class Sampler : ISampler
    {
        private readonly ID3D11SamplerState _sampler;
        public IntPtr NativeHandle => _sampler.Handle;
        public Sampler(ID3D11SamplerState sampler)
        {
            _sampler = sampler;
        }

        public void Dispose()
        {
            _sampler.Dispose();
        }
    }
}
