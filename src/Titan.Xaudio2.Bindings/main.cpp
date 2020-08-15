#include "main.h"

EXPORT HRESULT XAudio2Create_(
    IXAudio2** ppXAudio2, 
    UINT32 flags
)
{
    return XAudio2Create(ppXAudio2, flags, XAUDIO2_DEFAULT_PROCESSOR);
}

EXPORT HRESULT CreateMasteringVoice_(
    IXAudio2* ppXAudio2,
    IXAudio2MasteringVoice** ppMasteringVoice,
    UINT32 inputChannels,
    UINT32 inputSampleRate,
    UINT32 flags,
    LPCWSTR szDeviceId,
    const XAUDIO2_EFFECT_CHAIN* pEffectChain,
    AUDIO_STREAM_CATEGORY streamCategory) 
{
    return ppXAudio2->CreateMasteringVoice(ppMasteringVoice, inputChannels, inputSampleRate, flags, szDeviceId, pEffectChain, streamCategory);
}

EXPORT HRESULT CreateSourceVoice_(
    IXAudio2* ppXAudio2,
    IXAudio2SourceVoice** ppSourceVoice,
    const WAVEFORMATEX* pSourceFormat,
    UINT32 Flags,
    float MaxFrequencyRatio, // 2.0f
    IXAudio2VoiceCallback* pCallback,
    const XAUDIO2_VOICE_SENDS* pSendList,
    const XAUDIO2_EFFECT_CHAIN* pEffectChain)
{
    return ppXAudio2->CreateSourceVoice(ppSourceVoice, pSourceFormat, Flags, MaxFrequencyRatio, pCallback, pSendList, pEffectChain);
}


EXPORT HRESULT Start_(
    IXAudio2SourceVoice* handle,
    UINT32 flags,
    UINT32 operationSet)
{
    return handle->Start();
}

EXPORT HRESULT SubmitSourceBuffer_(
    IXAudio2SourceVoice* handle,
    XAUDIO2_BUFFER* pBuffer,
    const XAUDIO2_BUFFER_WMA* pBufferWMA) 
{
    return handle->SubmitSourceBuffer(pBuffer, pBufferWMA);
}


