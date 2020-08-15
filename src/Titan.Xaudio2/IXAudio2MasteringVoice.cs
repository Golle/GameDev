using System;

namespace Titan.Xaudio2
{
    public interface IXAudio2MasteringVoice : IDisposable
    {
    }

    internal class XAudio2MasteringVoice : ComPointer, IXAudio2MasteringVoice
    {
        public XAudio2MasteringVoice(IntPtr handle) 
            : base(handle)
        {
        }
    }
}
