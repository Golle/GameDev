using System.Runtime.InteropServices;

namespace Titan.Xaudio2.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Xaudio2EffectDescriptor
    {
        public unsafe void* Effect;                  // Pointer to the effect object's IUnknown interface.
        [MarshalAs(UnmanagedType.Bool)]
        public bool InitialState;                  // TRUE if the effect should begin in the enabled state.
        public uint OutputChannels;              // How many output channels the effect should produce.
    }
}
