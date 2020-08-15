using System;
using Titan.Xaudio2.Bindings;

namespace Titan.Xaudio2
{
    public interface IXAudio2SourceVoice : IDisposable
    {
        void SubmitSourceBuffer(in Xaudio2Buffer buffer);
        void Start();
    }
}
