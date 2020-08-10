using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Titan.Tools.FontGenerator.Models
{
    internal class TypefaceMetrics
    {
        //public Point BlackboxTopLeft => new Point(BearingLeft, BearingTop);
        //public Point BlackboxBottomRight => new Point(AdvanceX-BearingRight, BearingTop+Height);

        public double Baseline { get; set; }
        public double Height { get; set; }

        public IDictionary<char, GlyphMetrics> Glyphs { get; set; }

    }

    internal class GlyphMetrics
    {

        // TODO: add bounding box/black box
        public double BearingLeft { get; set; }
        public double BearingRight { get; set; }
        public double BearingTop { get; set; }
        public double BearingBottom { get; set; }
        public double AdvanceWidth { get; set; }
        public double AdvanceHeight { get; set; }
        public double BottomBlackboxDistance { get; set; }

        public GlyphBlackBox GetBlackBox(int fontSize) => new GlyphBlackBox(BearingLeft*fontSize, BearingTop*fontSize, (AdvanceWidth-BearingRight-BearingLeft)*fontSize, (AdvanceHeight-BearingBottom-BearingTop)*fontSize);
    }

    internal readonly ref struct GlyphBlackBox
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Width;
        public readonly double Height;
        public GlyphBlackBox(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
    

    internal class TypefaceMetricsReader
    {
        public TypefaceMetrics GetMetrics(string fontFamilyName, int fontSize, string characters, bool bold = false, bool italic = false)
        {
            var fontFamily = new FontFamily(fontFamilyName);
            var typefaces = fontFamily
                .GetTypefaces()
                .Where(f => f.Style == (italic ? FontStyles.Italic : FontStyles.Normal) && f.Weight == (bold ? FontWeights.Bold : FontWeights.Normal))
                .ToArray();

            if (typefaces.Count() > 1)
            {
                MessageBox.Show("More than a single typeface, picking first one.");
            }

            var typeface = typefaces.First();
            if (!typeface.TryGetGlyphTypeface(out var glyphTypeface))
            {
                throw new InvalidOperationException($"Failed to create GlyphTypeface for {fontFamilyName}");
            }

            var metrics = new TypefaceMetrics
            {
                Baseline = glyphTypeface.Baseline,
                Height = glyphTypeface.Height,
                Glyphs = characters.ToDictionary(c => c, c => CreateMetrics(glyphTypeface, glyphTypeface.CharacterToGlyphMap[c]))
            };
            
            return metrics;
        }

        private static GlyphMetrics CreateMetrics(GlyphTypeface typeface, ushort index)
        {
            return new GlyphMetrics
            {

                AdvanceHeight = typeface.AdvanceHeights[index],
                BearingTop = typeface.TopSideBearings[index],
                BearingRight = typeface.RightSideBearings[index],
                BearingBottom = typeface.BottomSideBearings[index],
                BearingLeft = typeface.LeftSideBearings[index],
                BottomBlackboxDistance = typeface.DistancesFromHorizontalBaselineToBlackBoxBottom[index],
                AdvanceWidth = typeface.AdvanceWidths[index],
            };
        }
    }
}
