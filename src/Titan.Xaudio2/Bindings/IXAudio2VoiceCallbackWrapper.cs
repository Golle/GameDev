using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Titan.Windows;

namespace Titan.Xaudio2.Bindings
{
    [SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
    public class IXAudio2VoiceCallbackWrapper : IDisposable
    {
        private readonly IXAudio2VoiceCallback _callback;
        private IntPtr _nativePointer;

        private readonly OnVoiceProcessingPassStartDelegate _startCallback;
        private readonly OnVoiceProcessingPassEnd _endCallback;
        private readonly OnStreamEnd _streamEndCallback;
        private readonly OnBufferStart _bufferStartCallback;
        private readonly OnBufferEnd _bufferEndCallback;
        private readonly OnLoopEnd _loopEndCallback;
        private readonly OnVoiceError _voiceErrorCallback;

        public IntPtr Pointer => _nativePointer;

        public IXAudio2VoiceCallbackWrapper(IXAudio2VoiceCallback callback)
        {
            _callback = callback;

            // Store callbacks to prevent them from being garbage collected
            _startCallback = _callback.OnVoiceProcessingPassStart;
            _endCallback = _callback.OnVoiceProcessingPassEnd;
            _streamEndCallback = _callback.OnStreamEnd;
            _bufferStartCallback = _callback.OnBufferStart;
            _bufferEndCallback = _callback.OnBufferEnd;
            _loopEndCallback = _callback.OnLoopEnd;
            _voiceErrorCallback = _callback.OnVoiceError;

            // Allocate memory for the C++ interface
            _nativePointer = Marshal.AllocHGlobal(IntPtr.Size * 8); // 7 methods + vtbl
            
            // Write pointer to vtbl
            Marshal.WriteIntPtr(_nativePointer, IntPtr.Add(_nativePointer, IntPtr.Size));

            // Write the functions to the vtable
            WriteFunctionPointer(1, _startCallback);
            WriteFunctionPointer(2, _endCallback);
            WriteFunctionPointer(3, _streamEndCallback);
            WriteFunctionPointer(4, _bufferStartCallback);
            WriteFunctionPointer(5, _bufferEndCallback);
            WriteFunctionPointer(6, _loopEndCallback);
            WriteFunctionPointer(7, _voiceErrorCallback);
        }

        private void WriteFunctionPointer(int offset, Delegate @delegate) => Marshal.WriteIntPtr(IntPtr.Add(_nativePointer, IntPtr.Size * offset), Marshal.GetFunctionPointerForDelegate(@delegate));

        public void Dispose()
        {
            if (_nativePointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_nativePointer);
                _nativePointer = IntPtr.Zero;
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnVoiceProcessingPassStartDelegate(uint bytesRequired);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnVoiceProcessingPassEnd();
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnStreamEnd();
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnBufferStart(IntPtr pBufferContext);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnBufferEnd(IntPtr pBufferContext);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnLoopEnd(IntPtr pBufferContext);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnVoiceError(IntPtr pBufferContext, HRESULT error);
    }
}
