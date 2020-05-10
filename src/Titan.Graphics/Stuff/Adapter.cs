using System;

namespace Titan.Graphics.Stuff
{
    /**
     *
     *
     * WIP
     *
     *
     */
    public interface IAdapter
    {
        IntPtr Handle { get; }
    }
    internal class Adapter
    {
        public IntPtr Handle { get; set; }
        public string Name { get; set; }

        public int RefreshRate { get; set; }
    }
}
