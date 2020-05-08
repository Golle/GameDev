#include "common.h"

EXPORT SIZE_T GetBufferSize_(
	ID3DBlob* blob
) 
{
	return blob->GetBufferSize();
}

EXPORT void * GetBufferPointer_(
	ID3DBlob* blob
)
{
	return blob->GetBufferPointer();
}
