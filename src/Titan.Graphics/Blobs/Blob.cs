using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Blobs
{
    public class Blob : IBlob
    {
        private readonly ID3DBlob _blob;
        public UIntPtr Size { get; }
        public IntPtr Buffer { get; }

        public Blob(ID3DBlob blob)
        {
            _blob = blob ?? throw new ArgumentNullException(nameof(blob));
            Size = blob.GetBufferSize();
            Buffer = blob.GetBufferPointer();
        }

        public void Dispose()
        {
            _blob.Dispose();
        }
    }
}
