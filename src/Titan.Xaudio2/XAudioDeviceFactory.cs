using System.ComponentModel;
using Titan.Xaudio2.Bindings;

namespace Titan.Xaudio2
{
    internal class XAudioDeviceFactory : IXAudioDeviceFactory
    {
        public IXAudio2 CreateDevice()
        {
            var result = IXAudio2Bindings.XAudio2Create_(out var handle, 0);
            if (result.Failed)
            {
                throw new Win32Exception($"Device CreateBuffer failed with code: 0x{result.Code.ToString("X")}");
            }
            return new XAudio2(handle);
        }
    }
}
