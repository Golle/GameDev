using System;
using Titan.Windows;

namespace Titan.Xaudio2.Bindings
{
    public interface IXAudio2VoiceCallback
    {
        void OnVoiceProcessingPassStart(uint bytesRequired);
        void OnVoiceProcessingPassEnd();
        void OnStreamEnd();
        void OnBufferStart(IntPtr pBufferContext);
        void OnBufferEnd(IntPtr pBufferContext);
        void OnLoopEnd(IntPtr pBufferContext);
        void OnVoiceError(IntPtr pBufferContext, HRESULT error);
    }
}
