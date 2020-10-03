Texture2D tex : register(t0);

SamplerState splr;

static const int MaxLights = 4;

cbuffer Lights : register(b0)
{
    float4 Positions[MaxLights];
    float4 Colors[MaxLights];
    uint NumberOfLights;
};

struct PS_INPUT
{
    float4 Color : Color;
    float3 Normal : Normal;
    float2 Texture : Texture;
    float4 WorldPosition : Position;
    float4 Position : SV_Position;
};


static const float3 DiffuseLightDirection = float3(0.0f, -0.5f, 0.5f);
static const float4 AmbientLightColor = float4(0.2f, 0.2f, 0.2f, 1.0f);
static const float4 DiffuseLightColor = float4(1.0f, 1.0f, 1.0f, 1.0f);

static const float3 DiffuseLightColor3 = float3(1.0f, 1.0f, 1.0f);
static const float3 AmbientLightColor3 = float3(0.2f, 0.2f, 0.2f);

static const float intensity = 10.0f;
float4 main(PS_INPUT input) : SV_TARGET
{
    //return float4(Positions[0], 1.0f);
    
    float3 totalLight = float3(0.0f, 0.0f, 0.0f);
    for (uint i = 0; i < NumberOfLights; ++i)
    {
        float4 lightDirection = Positions[i] - input.WorldPosition;
        float distance = length(lightDirection);
        
        float diffuseLightPercentage = saturate(dot(input.Normal, normalize(lightDirection.xyz)));
        float3 diffuseLight = saturate(DiffuseLightColor3 * diffuseLightPercentage) * intensity;
        totalLight += diffuseLight * intensity / distance;;
    }
        
    
    //float3 lightDirection = -DiffuseLightDirection;
    
    return tex.Sample(splr, input.Texture) * input.Color * float4(saturate(AmbientLightColor3 + totalLight), 1.0f);
}
