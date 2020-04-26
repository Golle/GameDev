#include "common.h"

EXTERN_C DLLEXPORT HRESULT D3D11SwapChainGetBuffer_(
	IDXGISwapChain* swapChain,
	UINT   buffer,
	REFIID riid,
	void** ppSurface
) 
{
	return swapChain->GetBuffer(buffer, riid, ppSurface);
}
