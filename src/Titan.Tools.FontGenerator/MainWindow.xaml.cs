using System.Windows;
using System.Windows.Input;
using Titan.Tools.FontGenerator.Angelfont;
using Titan.Tools.FontGenerator.Angelfont.Model;
using Titan.Tools.FontGenerator.Angelfont.Serialization;

namespace Titan.Tools.FontGenerator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Workaround to support updating the TextBox when clicking outside 
            Grid1.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var desc = new AngelfontDescription
            {
                Pages = new[] {new Page {File = "test_file1.png", Id = 1}},
                Characters = new[]
                {
                    new Character
                    {
                        Id = 'a',
                        Page = 0,
                        Y = 10,
                        X = 20,
                        Height = 30,
                        Width = 40,
                        YOffset = 50,
                        XAdvance = 60,
                        XOffset = 70,
                        Channel = Channel.All
                    },
                },
                Kernings = new[]
                {
                    new Kerning
                    {
                        First = 'a',
                        Second = 'b',
                        Amount = 10
                    },
                },
                Info = new Info
                {
                    Size = 50,
                    Padding = new Padding {Top = 1, Bottom = 2, Right = 3, Left = 4},
                    Spacing = new Spacing
                    {
                        Y = 10, X = 20
                    },
                    Italic = true,
                    Bold = false,
                    Unicode = true,
                    Charset = "utf8",
                    Smooth = true,
                    AntiAliasing = 3,
                    Face = "Segoe UI Light",
                    StretchH = 10
                },
                Common = new Common
                {
                    Packed = true,
                    Base = 10,
                    ScaleHeight = 20,
                    ScaleWidth = 30,
                    LineHeight = 40

                }
            };

            new TextSerializer()
                .SerializeAsync(desc, "c:/temp/")
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}
