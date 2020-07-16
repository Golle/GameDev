using Titan.Core.EventSystem;
using Titan.Windows.Window.Events;

namespace Titan.Windows.Input
{
    public class Mouse : IMouse
    {
        public Point Position { get; private set; }
        public Point DeltaMovement { get; private set; }
        public Point LastPosition { get; private set; }
        public bool LeftButtonDown { get; private set; }
        public bool RightButtonDown { get; private set; }
        
        public Mouse(IEventManager eventManager)
        {
            eventManager.Subscribe<MouseMovedEvent>(OnMouseMoved);
            eventManager.Subscribe<MouseLeftButtonPressedEvent>(OnLeftButtonPressed);
            eventManager.Subscribe<MouseLeftButtonReleasedEvent>(OnLeftButtonReleased);
            eventManager.Subscribe<MouseRightButtonPressedEvent>(OnRightButtonPressed);
            eventManager.Subscribe<MouseRightButtonReleasedEvent>(OnRightButtonReleased);
        }

        private void OnRightButtonReleased(in MouseRightButtonReleasedEvent @event)
        {
            RightButtonDown = false;
            Position = new Point(@event.X, @event.Y);
        }

        private void OnRightButtonPressed(in MouseRightButtonPressedEvent @event)
        {
            RightButtonDown = true;
            Position = new Point(@event.X, @event.Y);
        }

        private void OnLeftButtonReleased(in MouseLeftButtonReleasedEvent @event)
        {
            LeftButtonDown = false;
            Position = new Point(@event.X, @event.Y);
        }

        private void OnLeftButtonPressed(in MouseLeftButtonPressedEvent @event)
        {
            LeftButtonDown = true;
            Position = new Point(@event.X, @event.Y);
        }

        private void OnMouseMoved(in MouseMovedEvent @event)
        {
            Position = new Point(@event.X, @event.Y);
        }


        public void Update()
        {
            DeltaMovement = LastPosition - Position;
            LastPosition = Position;
        }
    }
}
