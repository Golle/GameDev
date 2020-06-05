using Titan.D3D11;
using Titan.D3D11.Device;

namespace Titan.Graphics
{
    internal class BlendState : IBlendState
    {
        private readonly ID3D11DeviceContext _context;
        private readonly ID3D11BlendState _blendState;

        public BlendState(ID3D11DeviceContext context, ID3D11BlendState blendState)
        {
            _context = context;
            _blendState = blendState;
        }

        public void Bind()
        {
            var blendFactor = new Color(1f, 1f, 1f, 1f);
            const uint sampleMask = 0xffffffff;
            _context.OMSetBlendState(_blendState, blendFactor, sampleMask);
        }

        public void Dispose()
        {
            _blendState.Dispose();
        }
    }
}
