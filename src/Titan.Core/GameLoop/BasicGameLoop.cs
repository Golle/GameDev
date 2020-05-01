using System;
using System.Diagnostics;
using System.Threading;
using Titan.Core.Configuration;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop.Events;

namespace Titan.Core.GameLoop
{
    internal class BasicGameLoop : IGameLoop
    {
        private readonly IEventManager _eventManager;
        private readonly int _fixedUpdateFrequency;
        public BasicGameLoop(IConfiguration configuration, IEventManager eventManager)
        {
            _eventManager = eventManager;
            _fixedUpdateFrequency = configuration.FixedUpdateFrequency;
        }

        public void Run(Func<bool> windowUpdate)
        {
            var frequency = Stopwatch.Frequency;
            // this should be done with ticks instead of time

            var fixedUpdateTicksPerSeconds = (long)((1f / _fixedUpdateFrequency) * frequency); 
            var fixedUpdateTickCount = 0L;
            
            var lastTick = Stopwatch.GetTimestamp();
            while (windowUpdate())
            {
                var currentTick = Stopwatch.GetTimestamp();
                var deltaTicks = currentTick - lastTick;

                
                fixedUpdateTickCount += deltaTicks;
                
                while (fixedUpdateTickCount >= fixedUpdateTicksPerSeconds)
                {
                    _eventManager.PublishImmediate(new FixedUpdateEvent(fixedUpdateTicksPerSeconds));
                    fixedUpdateTickCount -= fixedUpdateTicksPerSeconds;
                }

                _eventManager.PublishImmediate(new UpdateEvent(deltaTicks / (float)frequency, deltaTicks));

                _eventManager.Update();

                lastTick = currentTick;
            }
        }
    }
}
