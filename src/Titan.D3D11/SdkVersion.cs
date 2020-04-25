using System.Runtime.InteropServices;

namespace Titan.D3D11
{
    public class SdkVersion
    {
        [DllImport("Titan.D3D11.Bindings", EntryPoint = "D3D11SDKVersion")]
        public static extern uint GetVersion();
    }
}
