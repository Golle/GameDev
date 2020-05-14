using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Layout
{
    internal class InputLayout : IInputLayout
    {
        private readonly ID3D11DeviceContext _context;
        private readonly ID3D11InputLayout _inputLayout;

        public InputLayout(ID3D11DeviceContext context, ID3D11InputLayout inputLayout)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _inputLayout = inputLayout ?? throw new ArgumentNullException(nameof(inputLayout));
        }

        public void Bind()
        {
            _context.SetInputLayout(_inputLayout);
        }

        public void Dispose()
        {
            _inputLayout.Dispose();
        }
    }
}