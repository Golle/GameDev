using Titan.Core.Ioc;
using Titan.D3D11.Compiler;
using Titan.D3D11.Device;

namespace Titan.D3D11
{
    public class D3D11Registry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<ID3D11DeviceFactory, D3D11DeviceFactory>()
                .Register<ID3DCommon, D3DCommon>()
                .Register<ID3DCompiler, D3DCompiler>()

                ;
        }
    }
}
