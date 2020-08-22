using System;
using Titan.Xaudio2.Bindings;

namespace Titan.Xaudio2
{
    internal class XAudio2Voice : ComPointer, IXAudio2Voice
    {
        public XAudio2Voice(IntPtr handle) : base(handle)
        {
        }
        public void SetVolume(float volume)
        {
            var result = IXAudio2VoiceBindings.SetVolume_(Handle, volume);
            result.Check(nameof(XAudio2SourceVoice), nameof(SetVolume));
        }

        public float GetVolume()
        {
            IXAudio2VoiceBindings.GetVolume_(Handle, out var volume);
            return volume;
        }
    }
}
