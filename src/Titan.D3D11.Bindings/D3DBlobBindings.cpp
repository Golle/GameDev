#include "common.h"

EXTERN_C DLLEXPORT SIZE_T GetBufferSize_(
	ID3DBlob* blob
) 
{
	return blob->GetBufferSize();
}

EXTERN_C DLLEXPORT void * GetBufferPointer_(
	ID3DBlob* blob
)
{
	return blob->GetBufferPointer();
}
