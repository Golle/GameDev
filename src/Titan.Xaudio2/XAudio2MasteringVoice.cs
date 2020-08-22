using System;

namespace Titan.Xaudio2
{
    internal class XAudio2MasteringVoice : XAudio2Voice, IXAudio2MasteringVoice
    {
        public XAudio2MasteringVoice(IntPtr handle) 
            : base(handle)
        {
        }
    }
}
