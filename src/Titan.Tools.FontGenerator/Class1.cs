using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using Titan.Tools.FontGenerator.Angelfont;

namespace Titan.Tools.FontGenerator
{
    public struct SpriteSheetArguments
    {
        public int Width;
        public int Height;
        public int FontSize;
        public Padding Padding;
        public string FontName;
        public FontStyle FontStyle;
        public TextRenderingHint Rendering;
        public string Text;
    }

    public class FontSpriteSheetRenderer
    {
        public void DrawText(Graphics graphics, in SpriteSheetArguments args, bool drawDebugSquares = false)
        {
            using var font = new Font(args.FontName, args.FontSize, args.FontStyle, GraphicsUnit.Pixel);
            var r = CreateMeasurements(graphics, font, args.FontStyle);


            graphics.TextRenderingHint = args.Rendering;
            using var brush = new SolidBrush(Color.Black);
            using var bluePen = new Pen(Color.Blue);
            using var redPen = new Pen(Color.Red);


            var x = 0f;
            var y = 0f;

            graphics.DrawLine(new Pen(Color.Aqua), 0, r.DescentPixels, args.Width, r.DescentPixels);
            graphics.DrawLine(new Pen(Color.DarkGreen), 0, r.AscentPixels, args.Width, r.AscentPixels);
            //graphics.DrawLine(new Pen(Color.Red), 0, font.GetHeight(graphics) + y, args.Width, font.GetHeight(graphics) + y);
            foreach (var character in args.Text.Replace(Environment.NewLine, string.Empty))
            {

                var result = TestThis(character, font, args.Rendering);

                var padding = args.Padding;
                var genericTypographic = StringFormat.GenericTypographic;


                var characterSize =
                    graphics.MeasureString(character.ToString(), font, PointF.Empty, genericTypographic);
                //var characterSize1 = graphics.MeasureString(character.ToString(), font);
                //var characterSize2 = TextRenderer.MeasureText(graphics, character.ToString(), font, Size.Empty, TextFormatFlags.NoPadding);
                //var characterSize3 = TextRenderer.MeasureText(graphics, character.ToString(), font);
                //genericTypographic.SetMeasurableCharacterRanges(new CharacterRange[]{new CharacterRange(0, 1)});
                //var ranges = graphics.MeasureCharacterRanges(character.ToString(), font, new RectangleF(0, 0, 1000, 1000), genericTypographic);
                //var bounds = ranges[0].GetBounds(graphics);
                var glyphWidth = characterSize.Width + padding.Horizontal;
                var glyphHeight = characterSize.Height + padding.Vertical;


                //var fontHeight = font.Height;

                //var boundingBox = GDI.GetGlyphBox(character, font, graphics);


                if (x + glyphWidth > args.Width)
                {
                    y += glyphHeight;
                    x = 0f;
                    graphics.DrawLine(new Pen(Color.Aqua), 0, r.DescentPixels + y, args.Width, r.DescentPixels + y);
                    graphics.DrawLine(new Pen(Color.DarkGreen), 0, r.AscentPixels + y, args.Width, r.AscentPixels + y);
                    //graphics.DrawLine(new Pen(Color.Red), 0, r.LineSpacingPixels+y, args.Width, r.LineSpacingPixels + y);
                }

                graphics.DrawLine(bluePen, x, y, x, y + glyphHeight);
                graphics.DrawLine(redPen, x + glyphWidth, y, x + glyphWidth, y + glyphHeight);

                //if (drawDebugSquares)
                {
                    var theX = x + padding.Left + result.Item1; // + boundingBox.Item1;
                    var theY = y + padding.Top + result.Item2; // + boundingBox.Item2;
                    //graphics.DrawRectangle(redPen, theX, theY, 1, 1);  // Draw blue square for NON padded area
                    //graphics.DrawRectangle(redPen, theX, theY, result.Item3 - result.Item1, result.Item4-result.Item2);  // Draw blue square for NON padded area

                    //graphics.DrawRectangle(bluePen, x + padding.Left, y + padding.Top, characterSize.Width, characterSize.Height);  // Draw blue square for NON padded area
                    //graphics.DrawRectangle(redPen, x, y, glyphWidth, glyphHeight);                                                  // Draw red square for padded area

                    //graphics.DrawLine(new Pen(Color.Bisque), x, y+r.AscentPixels, x+characterSize.Width, y+r.AscentPixels);
                    //graphics.DrawLine(new Pen(Color.Red), x, y+r.DescentPixels, x+characterSize.Width, y+r.DescentPixels);
                }
                //graphics.DrawString(character.ToString(), font, brush, new RectangleF(x + args.Padding.Left, y + args.Padding.Top, boundingBox.Item1 + boundingBox.Item3, boundingBox.Item2 + boundingBox.Item4), StringFormat.GenericTypographic);

                graphics.DrawString(character.ToString(), font, brush, x + args.Padding.Left, y + args.Padding.Top,
                    StringFormat.GenericTypographic);

                //TextRenderer.DrawText(graphics, character.ToString(), font, new Point((int) (x+args.Padding.Left), (int) (y+args.Padding.Top)), Color.Black, Color.Transparent,TextFormatFlags.NoPadding |TextFormatFlags.Top);
                //graphics.DrawString(character.ToString(), font, brush, x + args.Padding.Left, y + args.Padding.Top, genericTypographic);
                x += glyphWidth;
                //break;
            }
        }


        private (int, int, int, int) TestThis(char character, Font font, TextRenderingHint hint)
        {
            //{
            //    using var bitmap = new Bitmap(font.Height * 2, font.Height);
            //    using var graphics = Graphics.FromImage(bitmap);
            //    graphics.Clear(Color.Transparent);

            //    graphics.TextRenderingHint = hint;
            //    graphics.DrawString($"WA", font, new SolidBrush(Color.Red), PointF.Empty, StringFormat.GenericTypographic);
            //    var measurement = graphics.MeasureString(character.ToString(), font, PointF.Empty, StringFormat.GenericTypographic);
            //    int minX = int.MaxValue, minY = int.MaxValue, maxX = 0, maxY = 0;
            //    for (var y = 0; y < bitmap.Height; ++y)
            //    {
            //        for (var x = 0; x < bitmap.Height; ++x)
            //        {
            //            if (bitmap.GetPixel(x, y).A != 0)
            //            {
            //                if (x < minX) minX = x;
            //                if (y < minY) minY = y;
            //                if (x > maxX) maxX = x;
            //                if (y > maxY) maxY = y;
            //            }
            //        }
            //    }
            //    //bitmap.Save(@"c:\temp\test1.png", ImageFormat.Png);
            //}
            {
                using var bitmap = new Bitmap(font.Height * 2, font.Height);
                using var graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.Transparent);

                graphics.TextRenderingHint = hint;
                graphics.DrawString($"{character}", font, new SolidBrush(Color.Red), PointF.Empty,
                    StringFormat.GenericTypographic);
                var measurement = graphics.MeasureString(character.ToString(), font, PointF.Empty,
                    StringFormat.GenericTypographic);
                int minX = int.MaxValue, minY = int.MaxValue, maxX = 0, maxY = 0;
                for (var y = 0; y < bitmap.Height; ++y)
                {
                    for (var x = 0; x < bitmap.Height; ++x)
                    {
                        if (bitmap.GetPixel(x, y).A != 0)
                        {
                            if (x < minX) minX = x;
                            if (y < minY) minY = y;
                            if (x > maxX) maxX = x;
                            if (y > maxY) maxY = y;
                        }
                    }
                }

                //bitmap.Save(@"c:\temp\test2.png", ImageFormat.Png);
                return (minX, minY, maxX, maxY);
            }
        }


        private FontMeasurements CreateMeasurements(Graphics graphics, Font font, FontStyle style)
        {
            Debug.Assert(font.Unit == GraphicsUnit.Pixel);
            var family = font.FontFamily;
            var emHeight = family.GetEmHeight(style);
            var designToPixels = font.Size / emHeight;

            return new FontMeasurements
            {
                EmHeightPixels = family.GetEmHeight(style),
                AscentPixels = family.GetCellAscent(style) * designToPixels,
                DescentPixels = family.GetCellDescent(style) * designToPixels,
                LineSpacingPixels = family.GetLineSpacing(style) * designToPixels,
            };


        }


    }

    internal class FontMeasurements
    {
        public float EmHeightPixels { get; set; }
        public float AscentPixels { get; set; }
        public float DescentPixels { get; set; }
        public float LineSpacingPixels { get; set; }
        public float CellHeightPixels => AscentPixels + DescentPixels;
        public float InternalLeadingPixels => CellHeightPixels - EmHeightPixels;
        public float ExternalLeadingPixels => LineSpacingPixels - CellHeightPixels;

        // Distances from the top of the cell in pixels.
        public float RelTop => InternalLeadingPixels;
        public float RelBaseline => AscentPixels;
        public float RelBottom => CellHeightPixels;
    }


}
