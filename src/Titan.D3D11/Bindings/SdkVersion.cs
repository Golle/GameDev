using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings
{
    public class SdkVersion
    {
        [DllImport(Constants.D3D11Dll, EntryPoint = "D3D11SDKVersion")]
        public static extern uint GetVersion();
    }
}
