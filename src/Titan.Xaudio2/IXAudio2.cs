using System;

namespace Titan.Xaudio2
{
    public interface IXAudio2 : IDisposable
    {
        IXAudio2MasteringVoice CreateMasteringVoice();
    }
}
