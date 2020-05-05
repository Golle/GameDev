#include "common.h"

EXTERN_C DLLEXPORT HRESULT GetBuffer_(
	IDXGISwapChain* swapChain,
	UINT   buffer,
	REFIID riid,
	void** ppSurface
) 
{
	return swapChain->GetBuffer(buffer, riid, ppSurface);
}

EXTERN_C DLLEXPORT void SwapChainPresent_(
	IDXGISwapChain* swapChain,
	UINT syncInterval,
	UINT flags
) 
{
	swapChain->Present(syncInterval, flags);
}


