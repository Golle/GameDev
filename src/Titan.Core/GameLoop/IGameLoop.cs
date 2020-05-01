using System;

namespace Titan.Core.GameLoop
{
    public interface IGameLoop
    {
        void Run(Func<bool> windowUpdate);
    }
}
