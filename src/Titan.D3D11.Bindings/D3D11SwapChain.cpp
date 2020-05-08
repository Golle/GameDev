#include "common.h"

EXPORT HRESULT GetBuffer_(
	IDXGISwapChain* swapChain,
	UINT   buffer,
	REFIID riid,
	void** ppSurface
) 
{
	return swapChain->GetBuffer(buffer, riid, ppSurface);
}

EXPORT void Present_(
	IDXGISwapChain* swapChain,
	UINT syncInterval,
	UINT flags
) 
{
	swapChain->Present(syncInterval, flags);
}


