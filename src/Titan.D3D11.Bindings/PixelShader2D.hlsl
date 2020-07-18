Texture2D tex : register(t0);

SamplerState splr;

struct PS_INPUT
{
    float2 Texture : Textures;
    float4 Color : Color;
    float4 Position : SV_Position;
};

float4 main(PS_INPUT input) : SV_TARGET
{
	return tex.Sample(splr, input.Texture) * input.Color;
}
