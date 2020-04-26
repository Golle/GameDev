namespace Titan.D3D11
{
    internal class D3D11Device : ID3D11Device
    {
        public ID3D11SwapChain SwapChain { get; }
        public D3D11Device(ID3D11SwapChain swapChain)
        {
            SwapChain = swapChain;
        }
    }
}
