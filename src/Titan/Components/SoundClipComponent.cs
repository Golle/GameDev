using System.Runtime.InteropServices;
using Titan.Sound;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SoundClipComponent
    {
        public ISoundClip Clip;
    }
}
