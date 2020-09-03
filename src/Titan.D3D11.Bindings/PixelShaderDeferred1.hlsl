
struct PS_INPUT
{
    float4 Color : Color;
    float4 Color1 : Color;
};


float4 main(PS_INPUT input) : SV_TARGET
{
	return input.Color * input.Color1;
}
