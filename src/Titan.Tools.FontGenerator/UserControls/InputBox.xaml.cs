using System.Windows;
using System.Windows.Controls;

namespace Titan.Tools.FontGenerator.UserControls
{
    public partial class InputBox : UserControl
    {
        public string Label
        {
            get => (string) GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public InputBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(InputBox), new PropertyMetadata(string.Empty));
    }
}
