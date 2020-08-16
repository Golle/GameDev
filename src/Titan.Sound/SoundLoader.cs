using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Titan.Core.Assets.Wave;
using Titan.Xaudio2;
using Titan.Xaudio2.Bindings;

namespace Titan.Sound
{
    internal class SoundLoader : ISoundLoader
    {
        private readonly IWaveReader _waveReader;
        private readonly IXAudioDeviceFactory _xAudioDeviceFactory;

        public SoundLoader(IWaveReader waveReader, IXAudioDeviceFactory xAudioDeviceFactory)
        {
            _waveReader = waveReader;
            _xAudioDeviceFactory = xAudioDeviceFactory;
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



            TestSound(asset);

            return new XAudio2SoundClip(asset);
        }

        //private IXAudio2SourceVoice voice;
        private IXAudio2 device;
        private IXAudio2MasteringVoice masteringVoice;
        private unsafe void TestSound(WaveData asset)
        {
            if(device == null)
            {
                device = _xAudioDeviceFactory.CreateDevice();
                masteringVoice = device.CreateMasteringVoice();


                var voice = device.CreateSourceVoice(asset.Format);
                //// Music
                Xaudio2Buffer buffer = default;
                buffer.Context = null;
                buffer.AudioBytes = asset.Size;
                buffer.AudioData = (byte*)asset.Data.ToPointer();
                buffer.Flags = Xaudio2Constants.Xaudio2EndOfStream;
                voice.SubmitSourceBuffer(buffer);
                voice.Start();
            }
            else
            {
                var voice = device.CreateSourceVoice(asset.Format);
                Task.Run(() =>
                {
                    while (true)
                    {
                        Xaudio2Buffer buffer = default;
                        buffer.Context = null;
                        buffer.AudioBytes = asset.Size;
                        buffer.AudioData = (byte*)asset.Data.ToPointer();
                        buffer.Flags = Xaudio2Constants.Xaudio2EndOfStream;
                        voice.SubmitSourceBuffer(buffer);
                        voice.Start();

                        Task.Delay(TimeSpan.FromSeconds(4)).Wait();
                        GC.Collect();
                    }
                });
                /*
                TODO
                    * Dont call COM Release on anything except the main XAUdio object.why ?
                    *On release, stop all sounds, release buffers and wait for all to stop(callbacks called) or it might crash.
                 * */
            }


        }
    }
}
