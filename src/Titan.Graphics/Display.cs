using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Windows.Window;

namespace Titan.Graphics
{

    /**
     *
     *
     * WIP
     *
     *
     */
    internal class Adapter
    {
        public IntPtr Handle { get; set; }
        public string Name { get; set; }

        public int RefreshRate { get; set; }
    }

    internal interface IDisplayFactory
    {
        IDisplay Create(string title, int width, int height);
        IDisplay Create(string title, int width, int height, Adapter adapter);
    }

    internal interface IDisplay
    {
        
    }

    internal class Display
    {
        public Display(string title, int width, int height, Adapter adapter)
        {
            
        }
        
        public Adapter[] Adapters { get; set; }


        

        public IWindow CreateWindow(string title, int width, int height, Adapter adapter)
        {
            throw new NotImplementedException();
        }
    }
}
