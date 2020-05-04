using System;
using Titan.Core.EventSystem;
using Titan.Windows.Win32;
using Titan.Windows.Window.Events;

namespace Titan.Windows.Input
{
    public class Mouse : IMouse
    {
        public Point Position => _position;
        private Point _position;
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
            _position.X = @event.X;
            _position.Y = @event.Y;
        }

        private void OnRightButtonPressed(in MouseRightButtonPressedEvent @event)
        {
            RightButtonDown = true;
            _position.X = @event.X;
            _position.Y = @event.Y;
        }

        private void OnLeftButtonReleased(in MouseLeftButtonReleasedEvent @event)
        {
            LeftButtonDown = false;
            _position.X = @event.X;
            _position.Y = @event.Y;
        }

        private void OnLeftButtonPressed(in MouseLeftButtonPressedEvent @event)
        {
            LeftButtonDown = true;
            _position.X = @event.X;
            _position.Y = @event.Y;
        }

        private void OnMouseMoved(in MouseMovedEvent @event)
        {
            _position.X = @event.X;
            _position.Y = @event.Y;
        }


        public void Update()
        {

            // noop at the moment
        }
    }
}
