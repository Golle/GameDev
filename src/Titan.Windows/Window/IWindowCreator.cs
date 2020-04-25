namespace Titan.Windows.Window
{
    public interface IWindowCreator 
    {
        IWindow CreateWindow(CreateWindowArguments arguments);
    }
}
