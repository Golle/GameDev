using System;
using Titan.Xaudio2;
using Titan.Xaudio2.Bindings;

namespace Titan.Sound
{
    internal class XAudio2Player : IDisposable
    {
        public bool IsPlaying { get; set; }

        private readonly IXAudio2SourceVoice _source;

        public XAudio2Player(IXAudio2SourceVoice source)
        {
            _source = source;
        }

        public void Play(in Xaudio2Buffer buffer)
        {
            _source.SubmitSourceBuffer(buffer);
            _source.Start();
        }

        public void Dispose()
        {
            _source.Dispose();
        }

        public void SetVolume(in float volume)
        {
            _source.SetVolume(volume);
        }
    }
}
