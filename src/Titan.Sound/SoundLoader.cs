using System;
using System.IO;
using Titan.Core.Assets.Wave;

namespace Titan.Sound
{
    internal class SoundLoader : ISoundLoader
    {
        private readonly IWaveReader _waveReader;

        public SoundLoader(IWaveReader waveReader)
        {
            _waveReader = waveReader;
        }

        public ISoundClip Load(string filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLowerInvariant();
            if (fileExtension != ".wav")
            {
                throw new NotSupportedException($"Sound format '{fileExtension}' is not supported.");
            }
            using var file = File.OpenRead(filename);
            var asset = _waveReader.ReadFromStream(file);
            return new XAudio2SoundClip(asset);
        }
    }
}
