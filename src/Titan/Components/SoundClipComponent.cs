using System.Runtime.InteropServices;
using Titan.Sound;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SoundClipComponent
    {
        public ISoundClip Clip;
    }
}
