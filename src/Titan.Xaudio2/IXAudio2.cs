using System;
using Titan.Core.Assets.Wave;

namespace Titan.Xaudio2
{
    public interface IXAudio2 : IDisposable
    {
        IXAudio2MasteringVoice CreateMasteringVoice();

        IXAudio2SourceVoice CreateSourceVoice(in WaveformatEx format);
    }
}
