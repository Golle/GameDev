using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Layout
{
    internal class InputLayout : IInputLayout
    {
        private readonly ID3D11InputLayout _inputLayout;
        public IntPtr NativeHandle => _inputLayout.Handle;
        public InputLayout(ID3D11InputLayout inputLayout)
        {
            _inputLayout = inputLayout ?? throw new ArgumentNullException(nameof(inputLayout));
        }

        public void Dispose()
        {
            _inputLayout.Dispose();
        }
    }
}
