using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using Titan.Tools.FontGenerator.Models;

namespace Titan.Tools.FontGenerator
{
    public class FontSpriteSheetRenderer2
    {
        private readonly TypefaceMetricsReader _metricsReader = new TypefaceMetricsReader();

        public void DrawText(Graphics graphics, in SpriteSheetArguments args, bool drawDebugSquares = false)
        {
            var argsFontSize = 80;//args.FontSize;
            using var font = new Font(args.FontName, argsFontSize, args.FontStyle, GraphicsUnit.Pixel);
            graphics.TextRenderingHint = args.Rendering;

            using var brush = new SolidBrush(Color.Black);
            using var bluePen = new Pen(Color.FromArgb(255, 0, 0, 255));
            using var redPen = new Pen(Color.Red);
            using var pen = new Pen(Color.Chartreuse);

            var x = 0f;
            var y = 0f;

            var characters = args.Text.Replace(Environment.NewLine, string.Empty);
            var metrics = _metricsReader.GetMetrics(args.FontName, argsFontSize, characters, false, false);

            var baseline = (float)(metrics.Baseline * argsFontSize);
            var height = (float)(metrics.Height * argsFontSize);

            //graphics.DrawLine(redPen, 10, 0, 10, 200);
            //graphics.DrawString('j'.ToString(), font, brush, (float)(-metrics.Glyphs['j'].BearingLeft * args.FontSize), 0, StringFormat.GenericTypographic);
            //var key = 'H';
            //var glyphMetrics = metrics.Glyphs[key];
            //var offset = (float)(glyphMetrics.BearingLeft * argsFontSize);
            //graphics.DrawString(key.ToString(), font, brush, 10f - offset, 0, StringFormat.GenericTypographic);

            foreach (var character in characters)
            {
                //var padding = args.Padding;
                //var genericTypographic = StringFormat.GenericTypographic;

                //var characterSize = graphics.MeasureString(character.ToString(), font, PointF.Empty, genericTypographic);

                //var glyphWidth = characterSize.Width + padding.Horizontal;
                //var glyphHeight = characterSize.Height + padding.Vertical;

                
                var metricsGlyph = metrics.Glyphs[character];
                //var glyphWidth = (float)((metricsGlyph.AdvanceWidth * args.FontSize) - (metricsGlyph.BearingLeft*args.FontSize) - (metricsGlyph.BearingRight*args.FontSize));
                //
                var glyphWidth = (float)((metricsGlyph.AdvanceWidth - metricsGlyph.BearingLeft - metricsGlyph.BearingRight) * argsFontSize);
                if (character == 'j' || character == 'i' ||character == 'H')
                {
                    Debug.WriteLine($"{character} = {metricsGlyph.BearingRight} {metricsGlyph.BearingLeft}");
                }




                if (x + glyphWidth > args.Width)
                {
                    y += height;
                    x = 0f;
                    //graphics.DrawLine(new Pen(Color.Aqua), 0, (int)Math.Round(metrics.Baseline*args.FontSize + y), args.Width, (int)Math.Round( metrics.Baseline * args.FontSize + y));
                    //graphics.DrawLine(new Pen(Color.DarkGreen), 0, r.AscentPixels + y, args.Width, r.AscentPixels + y);
                    //graphics.DrawLine(new Pen(Color.Red), 0, r.LineSpacingPixels+y, args.Width, r.LineSpacingPixels + y);
                }



                //graphics.DrawLine(pen, 0, baseline + y, args.Width, baseline + y);

                var offset = (float)(metricsGlyph.BearingLeft * argsFontSize);
                {
                    
                    var blackbox = metricsGlyph.GetBlackBox(argsFontSize);
                    //var advanceWidth = (float)(metrics.Glyphs[character].AdvanceWidth * args.FontSize);
                    //var advanceHeight = (float) (metrics.Glyphs[character].AdvanceHeight * args.FontSize);

                    var dis = (float)(metricsGlyph.BottomBlackboxDistance * argsFontSize);

                    graphics.DrawRectangle(redPen, (float)(x + blackbox.X), (float)(y + baseline + dis - blackbox.Height), (float)blackbox.Width, (float)blackbox.Height);
                }
                
                //graphics.DrawLine(bluePen, 0, height + y, args.Width, height + y);
                //graphics.DrawLine(bluePen, 0, y, args.Width, y);
                //graphics.DrawLine(bluePen, 0, height + y, args.Width, height + y);
                //graphics.DrawLine(bluePen, 0, y, args.Width, y);
                //graphics.DrawLine(redPen, x + glyphWidth, y, x + glyphWidth, y + glyphHeight);

                //if (drawDebugSquares)
                {
                    //var theX = x + padding.Left + result.Item1; // + boundingBox.Item1;
                    //var theY = y + padding.Top + result.Item2; // + boundingBox.Item2;
                    //graphics.DrawRectangle(redPen, theX, theY, 1, 1);  // Draw blue square for NON padded area
                    //graphics.DrawRectangle(redPen, theX, theY, result.Item3 - result.Item1, result.Item4-result.Item2);  // Draw blue square for NON padded area

                    //graphics.DrawRectangle(bluePen, x + padding.Left, y + padding.Top, characterSize.Width, characterSize.Height);  // Draw blue square for NON padded area
                    //graphics.DrawRectangle(redPen, x, y, glyphWidth, glyphHeight);                                                  // Draw red square for padded area

                    //graphics.DrawLine(new Pen(Color.Bisque), x, y+r.AscentPixels, x+characterSize.Width, y+r.AscentPixels);
                    //graphics.DrawLine(new Pen(Color.Red), x, y+r.DescentPixels, x+characterSize.Width, y+r.DescentPixels);
                }
                //graphics.DrawString(character.ToString(), font, brush, new RectangleF(x + args.Padding.Left, y + args.Padding.Top, boundingBox.Item1 + boundingBox.Item3, boundingBox.Item2 + boundingBox.Item4), StringFormat.GenericTypographic);

                
                graphics.DrawString(character.ToString(), font, brush, x + args.Padding.Left - offset, y + args.Padding.Top, StringFormat.GenericTypographic);

                //TextRenderer.DrawText(graphics, character.ToString(), font, new Point((int) (x+args.Padding.Left), (int) (y+args.Padding.Top)), Color.Black, Color.Transparent,TextFormatFlags.NoPadding |TextFormatFlags.Top);
                //graphics.DrawString(character.ToString(), font, brush, x + args.Padding.Left, y + args.Padding.Top, genericTypographic);
                x += glyphWidth;
                //break;
            }
        }
    }
}
