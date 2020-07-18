namespace Titan.Components.UI
{
    public delegate void OnInteract();
    public delegate void OnMouseEnter();
    public delegate void OnMouseLeave();

    public struct Button 
    {
        public OnInteract OnInteract;
        public OnMouseEnter OnMouseEnter;
        public OnMouseLeave OnMouseLeave;
        //public IMouseListener MouseListener;
    }

    //public interface IMouseListener
    //{
    //    public void OnInteract();
    //    public void OnMouseEnter();
    //    public void OnMouseLeave();
    //}
}
