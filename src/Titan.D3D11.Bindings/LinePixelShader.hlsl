struct PS_INPUT
{
    float4 Color : Color;
    float4 Position : SV_Position;
};

float4 main(PS_INPUT input) : SV_TARGET
{
    return input.Color;
}
