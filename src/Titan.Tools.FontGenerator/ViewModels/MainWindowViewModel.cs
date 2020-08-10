using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Drawing.Brushes;
using FontFamily = System.Drawing.FontFamily;
using FontStyle = System.Drawing.FontStyle;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.Point;

namespace Titan.Tools.FontGenerator.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {

        private SpriteSheetArguments _arguments = new SpriteSheetArguments
        {
            Height = 512, 
            Width = 512, 
            FontSize = 40, 
            FontStyle = FontStyle.Regular,
            Rendering = TextRenderingHint.SystemDefault, 
            FontName = "Segoe UI Light",
            Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ\r\nabcdefghijklmnopqrstuvwxyzåäö\r\n1234567890\r\n\"!`?'.,;:()[]{}<>|/@\\^$-%+=#_&~*"
        };

        private readonly FontSpriteSheetRenderer _renderer = new FontSpriteSheetRenderer();
        private readonly FontSpriteSheetRenderer2 _renderer2 = new FontSpriteSheetRenderer2();
        private bool _showBorders;

        public IEnumerable<string> FontNames => FontFamily.Families.Select(f => f.Name).OrderBy(f => f);
        public IEnumerable<int> FontSizes => Enumerable.Range(10, 100);
        public IEnumerable<TextRenderingHint> RenderingHints => ((TextRenderingHint[])Enum.GetValues(typeof(TextRenderingHint))).OrderBy(value => value.ToString());
        public IEnumerable<FontStyle> FontStyles => ((FontStyle[])Enum.GetValues(typeof(FontStyle))).OrderBy(value => value.ToString());

        public string SelectedFontName
        {
            get => _arguments.FontName;
            set
            {
                if (value != _arguments.FontName)
                {
                    _arguments.FontName = value;
                    OnPropertyChanged(nameof(FontSheet));
                }
            }
        }

        public TextRenderingHint SelectedTextRendering
        {
            get => _arguments.Rendering;
            set
            {
                _arguments.Rendering = value;
                OnPropertyChanged(nameof(FontSheet));
            }
        }

        public FontStyle SelectedFontStyle
        {
            get => _arguments.FontStyle;
            set
            {
                _arguments.FontStyle = value;
                OnPropertyChanged(nameof(FontSheet));
            }
        }

        public bool ShowBorders
        {
            get => _showBorders;
            set
            {
                _showBorders = value;
                OnPropertyChanged(nameof(FontSheet));
            }
        }

        public string Text
        {
            get => _arguments.Text;
            set
            {
                _arguments.Text = value;
                OnPropertyChanged(nameof(FontSheet));
            }
        }

        public int SelectedFontSize
        {
            get => _arguments.FontSize;
            set
            {
                _arguments.FontSize = value;
                OnPropertyChanged(nameof(FontSheet));
            }
        }

        // TODO: use this when we've got it up and running instead of Bitmap
        //public RenderTargetBitmap Apa()
        //{
        //    var bitmap = new RenderTargetBitmap(_arguments.Width, _arguments.Height, 72, 72, PixelFormats.Pbgra32);

            
        //    var visual = new DrawingVisual();

        //    using (var r = visual.RenderOpen())
        //    {
                
        //        r.DrawImage(bitmap, new Rect(0, 0, bitmap.Width, bitmap.Height));
        //        r.DrawLine(new System.Windows.Media.Pen(System.Windows.Media.Brushes.Red, 10.0), new System.Windows.Point(0, 0), new System.Windows.Point(bitmap.Width, bitmap.Height));
        //        r.DrawText(new FormattedText("Hello", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 24.0, System.Windows.Media.Brushes.Black), new System.Windows.Point(100, 10));
        //    }
        //    bitmap.Render(visual);
            
        //    return bitmap;
        //}

        //private BitmapSource _bitmap;

        public BitmapSource FontSheet
        {
            get
            {
                using var bitmap = new Bitmap(_arguments.Width, _arguments.Height);
                using var graphics = Graphics.FromImage(bitmap);
                _renderer2.DrawText(graphics, _arguments);
                return ToBitmapImage(bitmap);
            }
        }
        private static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using var memory = new MemoryStream();

            bitmap.Save(memory, ImageFormat.Png);
            memory.Position = 0;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }
    }
}
