using System.ComponentModel;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3DCommon : ID3DCommon
    {
        public ID3DBlob ReadFileToBlob(string fileName)
        {
            var result = D3D11CommonBindings.D3DReadFileToBlob_(fileName, out var ppContents);
            if (result.Failed)
            {
                throw new Win32Exception($"ReadFileToBlob failed with code: 0x{result.Code.ToString("X")}");
            }
            return new D3DBlob(ppContents);
        }
    }
}
