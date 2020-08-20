using System;
using Titan.Xaudio2.Bindings;

namespace Titan.Xaudio2
{
    internal class XAudio2MasteringVoice : ComPointer, IXAudio2MasteringVoice
    {
        public XAudio2MasteringVoice(IntPtr handle) 
            : base(handle)
        {
        }

        public void SetVolume(float volume)
        {
            var result = IXAudio2VoiceBindings.SetVolume_(Handle, volume);
            result.Check(nameof(XAudio2MasteringVoice), nameof(SetVolume));
        }
    }
}
