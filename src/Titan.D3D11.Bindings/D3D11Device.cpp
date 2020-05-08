#include "common.h"

EXPORT HRESULT D3D11CreateDevice_(
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

EXPORT HRESULT D3D11CreateDeviceAndSwapChain_(
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


EXPORT HRESULT CreateRenderTargetView_(
	ID3D11Device*				device,
	ID3D11Resource*				pResource,
	const D3D11_RENDER_TARGET_VIEW_DESC* pDesc,
	ID3D11RenderTargetView**	ppRTView
)
{
	return device->CreateRenderTargetView(pResource, pDesc, ppRTView);
}
EXPORT HRESULT CreateBuffer_(
	ID3D11Device* device,
	const D3D11_BUFFER_DESC* pDesc,
	const D3D11_SUBRESOURCE_DATA* pInitialData,
	ID3D11Buffer** ppBuffer)
{
	return device->CreateBuffer(pDesc, pInitialData, ppBuffer);
}

EXPORT HRESULT CreateVertexShader_(
	ID3D11Device * device,
	const void* pShaderBytecode,
	SIZE_T BytecodeLength,
	ID3D11ClassLinkage* pClassLinkage,
	ID3D11VertexShader** ppVertexShader
)
{
	return device->CreateVertexShader(pShaderBytecode, BytecodeLength, pClassLinkage, ppVertexShader);
}


EXPORT HRESULT CreatePixelShader_(
	ID3D11Device* device,
	const void* pShaderBytecode,
	SIZE_T BytecodeLength,
	ID3D11ClassLinkage* pClassLinkage,
	ID3D11PixelShader** ppPixelShader
)
{
	return device->CreatePixelShader(pShaderBytecode, BytecodeLength, pClassLinkage, ppPixelShader);
}

EXPORT HRESULT CreateInputLayout_(
	ID3D11Device* device,
	const D3D11_INPUT_ELEMENT_DESC* pInputElementDescs,
	UINT NumElements,
	const void* pShaderBytecodeWithInputSignature,
	SIZE_T bytecodeLength,
	ID3D11InputLayout** ppInputLayout
)
{
	return device->CreateInputLayout(pInputElementDescs, NumElements, pShaderBytecodeWithInputSignature, bytecodeLength, ppInputLayout);
}

EXPORT HRESULT CreateDepthStencilState_(
	ID3D11Device* device,
	const D3D11_DEPTH_STENCIL_DESC* pDepthStencilDesc,
	ID3D11DepthStencilState** ppDepthStencilState
)
{
	return device->CreateDepthStencilState(pDepthStencilDesc, ppDepthStencilState);
}

EXPORT HRESULT CreateTexture2D_(
	ID3D11Device* device,
	const D3D11_TEXTURE2D_DESC* pDesc,
	const D3D11_SUBRESOURCE_DATA* pInitialData,
	ID3D11Texture2D** ppTexture2D
) 
{
	return device->CreateTexture2D(pDesc, pInitialData, ppTexture2D);
}


EXPORT HRESULT CreateDepthStencilView_(
	ID3D11Device* device,
	ID3D11Resource* pResource,
	const D3D11_DEPTH_STENCIL_VIEW_DESC* pDesc,
	ID3D11DepthStencilView** ppDepthStencilView
)
{
	return device->CreateDepthStencilView(pResource, pDesc, ppDepthStencilView);
}
