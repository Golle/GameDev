using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

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
            FontName = "Arial",
            Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ\r\nabcdefghijklmnopqrstuvwxyzåäö\r\n1234567890\r\n\"!`?'.,;:()[]{}<>|/@\\^$-%+=#_&~*"
        };

        private readonly FontSpriteSheetRenderer _renderer = new FontSpriteSheetRenderer();
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

        public BitmapSource FontSheet
        {
            get
            {
                using var bitmap = new Bitmap(_arguments.Width, _arguments.Height);
                using var graphics = Graphics.FromImage(bitmap);
                _renderer.DrawText(graphics, _arguments);
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
