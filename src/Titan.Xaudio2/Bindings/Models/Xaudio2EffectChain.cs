using System.Runtime.InteropServices;

namespace Titan.Xaudio2.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Xaudio2EffectChain
    {
        public uint EffectCount;                 // Number of effects in this voice's effect chain.
        [MarshalAs(UnmanagedType.LPArray)]
        public unsafe Xaudio2EffectDescriptor* EffectDescriptors; // Array of effect descriptors.
    }
}
