#include "common.h"


EXTERN_C DLLEXPORT void DeviceContextOMSetRenderTargets_(
	ID3D11DeviceContext*	context,
	UINT					numViews,
	ID3D11RenderTargetView* const* ppRenderTargetViews,
	ID3D11DepthStencilView* pDepthStencilView
) {
	context->OMSetRenderTargets(numViews, ppRenderTargetViews, pDepthStencilView);
}


EXTERN_C DLLEXPORT void DeviceContextClearRenderTargetView_(
	ID3D11DeviceContext* context,
	ID3D11RenderTargetView* pRenderTargetView,
	const FLOAT ColorRGBA[4]
) 
{
	context->ClearRenderTargetView(pRenderTargetView, ColorRGBA);
}

