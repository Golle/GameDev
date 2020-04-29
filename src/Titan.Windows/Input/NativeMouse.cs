namespace Titan.Windows.Input
{
    internal class NativeMouse : IMouse
    {
        public Point Position => _mousePosition;
        
        private Point _mousePosition;
        public bool LeftButtonDown { get; private set; }
        public bool RightButtonDown { get; private set; }

        public void OnMouseMove((int x, int y) position)
        {
            SetPosition(position);
        }

        public void OnLeftMouseButtonDown((int x, int y) position)
        {
            SetPosition(position);
            LeftButtonDown = true;
        }

        public void OnLeftMouseButtonUp((int x, int y) position)
        {
            SetPosition(position);
            LeftButtonDown = false;
        }

        public void OnRightMouseButtonDown((int x, int y) position)
        {
            SetPosition(position);
            RightButtonDown = true;
        }

        public void OnRightMouseButtonUp((int x, int y) position)
        {
            SetPosition(position);
            RightButtonDown = false;
        }

        private void SetPosition((int x, int y) position)
        {
            var (x, y) = position;
            _mousePosition.X = x;
            _mousePosition.Y = y;
        }
    }
}
