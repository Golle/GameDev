Texture2D tex : register(t0);
SamplerState splr : register(s0);

struct PS_INPUT
{
     float2 Texture : TEXCOORD;
     float4 Position : SV_Position;
};

float4 main(PS_INPUT input) : SV_TARGET
{
     // return float4(0.0f, 1.0f, 0.0f,1.0f);
 	return tex.Sample(splr, input.Texture);
}