Texture2D tex : register(t0);

SamplerState splr;


float4 main(float2 texCoords : TexCoord) : SV_TARGET
{
	return tex.Sample(splr, texCoords);
}
