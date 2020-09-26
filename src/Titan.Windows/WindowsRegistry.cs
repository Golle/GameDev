using Titan.IOC;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan.Windows
{
    public class WindowsRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<IInputManager, InputManager>()
                .Register<IWindowCreator, WindowCreator>()

                ;
        }
    }
}
