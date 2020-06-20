using Titan.D3D11.Device;

namespace Titan.Graphics.Textures
{
    internal class Sampler : ISampler
    {
        private readonly ID3D11DeviceContext _context;
        private readonly ID3D11SamplerState _sampler;

        public Sampler(ID3D11DeviceContext context, ID3D11SamplerState sampler)
        {
            _context = context;
            _sampler = sampler;
        }

        public void Dispose()
        {
            _sampler.Dispose();
        }

        public void Bind(uint startSlot = 0u)
        {
            _context.PSSetSamplers(startSlot, _sampler);
        }
    }
}