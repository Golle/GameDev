using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Titan.Tools.FontBuilder
{
    public partial class Form1 : Form
    {
        private SpriteSheetArguments _sheet;

        private readonly FontSpriteSheetRenderer _spriteSheet = new FontSpriteSheetRenderer();
        private const string DefaultText = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ\r\nabcdefghijklmnopqrstuvwxyzåäö\r\n1234567890\r\n\"!`?'.,;:()[]{}<>|/@\\^$-%+=#_&~*";
        public Form1()
        {
            var a = Environment.NewLine;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Text
            SpriteText.Text = DefaultText;
            SpriteText.LostFocus += UpdateSpriteSheet;
            
            // Font list
            Fonts.HeaderStyle = ColumnHeaderStyle.None;
            Fonts.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            Fonts.Items.AddRange(FontFamily.Families.Select(f => new ListViewItem(f.Name)).ToArray());
            Fonts.SelectedIndexChanged += UpdateSpriteSheet;


            // Font size
            FontSize.Value = 40;
            FontSize.ValueChanged += UpdateSpriteSheet;
            
            // Test rendering
            TextRendering.Items.AddRange(Enum.GetNames(typeof(TextRenderingHint)));
            TextRendering.SelectedIndex = 0;
            TextRendering.Update();
            TextRendering.SelectedIndexChanged += UpdateSpriteSheet;

            // Font styles
            FontStyles.Items.AddRange(Enum.GetNames(typeof(FontStyle)));
            FontStyles.SelectedIndex = 0;
            FontStyles.Update();
            FontStyles.SelectedIndexChanged += UpdateSpriteSheet;


            // Font sprite sheet
            FontSpriteSheet.Paint += (_, eventArgs) => _spriteSheet.DrawText(SpriteText.Text, eventArgs.Graphics, _sheet, ShowBorders.Checked);

            // Padding
            ConfigurePadding(PaddingBottom);
            ConfigurePadding(PaddingTop);
            ConfigurePadding(PaddingLeft);
            ConfigurePadding(PaddingRight);

            // Show border
            ShowBorders.CheckStateChanged += UpdateSpriteSheet;

            // Height
            SpriteSheetHeight.ValueChanged += SetBitmapSize;
            
            // Width
            SpriteSheetWidth.ValueChanged += SetBitmapSize;

            // Export button
            ExportButton.Click += ExportFont;

            SetBitmapSize(sender, e);
        }

        private void ConfigurePadding(NumericUpDown padding)
        {
            padding.ValueChanged += UpdateSpriteSheet;
            padding.Minimum = -20;
            padding.Maximum = 20;
        }


        private void SetBitmapSize(object sender, EventArgs e)
        {
            FontSpriteSheet.Image = new Bitmap((int)SpriteSheetWidth.Value, (int)SpriteSheetHeight.Value);
            FontSpriteSheet.Size = new Size((int)SpriteSheetWidth.Value, (int)SpriteSheetHeight.Value);
            UpdateSpriteSheet(sender, e);
        }


        private void UpdateSpriteSheet(object sender, EventArgs e)
        {
            _sheet.Height = (int)SpriteSheetHeight.Value;
            _sheet.Width = (int) SpriteSheetWidth.Value;
            _sheet.FontName = Fonts.SelectedItems.Count > 0 ? Fonts.SelectedItems[0].Text : string.Empty;
            _sheet.FontStyle = Enum.TryParse<FontStyle>(FontStyles.SelectedItem.ToString(), out var fontStyle) ? fontStyle : FontStyle.Regular;
            _sheet.FontSize = (int)FontSize.Value;
            _sheet.Padding.Left = (int)PaddingLeft.Value;
            _sheet.Padding.Right = (int)PaddingRight.Value;
            _sheet.Padding.Top = (int)PaddingTop.Value;
            _sheet.Padding.Bottom = (int)PaddingBottom.Value;
            _sheet.Rendering = Enum.TryParse<TextRenderingHint>(TextRendering.SelectedItem.ToString(), out var rendering) ? rendering : TextRenderingHint.SystemDefault;
            
            FontSpriteSheet.Refresh();
        }

        private void ExportFont(object? sender, EventArgs e)
        {
            using var bitmap = new Bitmap(_sheet.Width, _sheet.Height);
            using var graphics = Graphics.FromImage(bitmap);
            _spriteSheet.DrawText(SpriteText.Text, graphics, _sheet);
            bitmap.Save(@$"c:\temp\image_{DateTime.UtcNow.Ticks}.png", ImageFormat.Png);
        }
    }
}
