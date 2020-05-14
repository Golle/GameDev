using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Blobs
{
    internal class BlobReader : IBlobReader
    {
        private readonly ID3DCommon _common;

        public BlobReader(ID3DCommon common)
        {
            _common = common ?? throw new ArgumentNullException(nameof(common));
        }
        public IBlob ReadFromFile(string fileName)
        {
            var blob = _common.ReadFileToBlob(fileName);
            return new Blob(blob);
        }
    }
}
