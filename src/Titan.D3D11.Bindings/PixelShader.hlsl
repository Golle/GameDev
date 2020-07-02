Texture2D tex : register(t0);

SamplerState splr;

static const int MaxLights = 4;

cbuffer Lights : register(b0)
{
    float3 Positions[MaxLights];
    float3 Colors[MaxLights];
};

struct PS_INPUT
{
    float4 Color : Color;
    float3 Normal : Normal;
    float2 Texture : Texture;
    float4 Position : SV_Position;
};


static const float3 DiffuseLightDirection = float3(0.0f, -0.5f, 0.5f);
static const float4 AmbientLightColor = float4(0.2f, 0.2f, 0.2f, 1.0f);
static const float4 DiffuseLightColor = float4(1.0f, 1.0f, 1.0f, 1.0f);


float4 main(PS_INPUT input) : SV_TARGET
{
    float3 lightDirection = -DiffuseLightDirection;
    float diffuseLightPercentage = saturate(dot(input.Normal, lightDirection));
    float4 diffuseLight = saturate(DiffuseLightColor * diffuseLightPercentage);
    
    return tex.Sample(splr, input.Texture) * saturate(AmbientLightColor + diffuseLight);
}
