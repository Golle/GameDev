#include "common.h"

EXTERN_C DLLEXPORT HRESULT D3D11CreateDevice_(
	IDXGIAdapter*			pAdapter,
	D3D_DRIVER_TYPE         driverType,
	HMODULE                 software,
	UINT                    flags,
	const D3D_FEATURE_LEVEL* pFeatureLevels,
	UINT                    featureLevels,
	UINT                    SDKVersion,
	ID3D11Device**			ppDevice,
	D3D_FEATURE_LEVEL*		pFeatureLevel,
	ID3D11DeviceContext**	ppImmediateContext
)
{
	return D3D11CreateDevice(pAdapter, driverType, software, flags, pFeatureLevels, featureLevels, SDKVersion, ppDevice, pFeatureLevel, ppImmediateContext);
}

EXTERN_C DLLEXPORT HRESULT D3D11CreateDeviceAndSwapChain_(
	IDXGIAdapter*				pAdapter,
	D3D_DRIVER_TYPE				driverType,
	HMODULE						software,
	UINT						flags,
	const D3D_FEATURE_LEVEL*	pFeatureLevels,
	UINT						featureLevels,
	UINT						SDKVersion,
	DXGI_SWAP_CHAIN_DESC*		pSwapChainDesc,
	IDXGISwapChain**			ppSwapChain,
	ID3D11Device**				ppDevice,
	D3D_FEATURE_LEVEL*			pFeatureLevel,
	ID3D11DeviceContext**		ppImmediateContext
)
{
	return D3D11CreateDeviceAndSwapChain(pAdapter, driverType, software, flags, pFeatureLevels, featureLevels, SDKVersion, pSwapChainDesc, ppSwapChain, ppDevice, pFeatureLevel, ppImmediateContext);
}


EXTERN_C DLLEXPORT HRESULT D3D11CreateRenderTargetView_(
	ID3D11Device*				device,
	ID3D11Resource*				pResource,
	const D3D11_RENDER_TARGET_VIEW_DESC* pDesc,
	ID3D11RenderTargetView**	ppRTView
)
{
	return device->CreateRenderTargetView(pResource, pDesc, ppRTView);
}
