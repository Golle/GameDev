namespace Titan.D3D11.Device
{
    public interface ID3D11DeviceFactory
    {
        ID3D11Device Create(CreateDeviceArguments arguments);
    }
}
