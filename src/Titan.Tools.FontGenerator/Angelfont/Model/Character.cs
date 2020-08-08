namespace Titan.Tools.FontGenerator.Angelfont.Model
{
    public class Character
    {
        // char id=0       x=134  y=356  width=46   height=62   xoffset=4    yoffset=30   xadvance=57   page=0    chnl=0 
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int XAdvance { get; set; }
        public int Page { get; set; }
        public Channel Channel { get; set; }

    }
}
