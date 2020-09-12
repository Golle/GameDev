using System.Windows;
using System.Windows.Controls;

namespace Titan.Tools.FontGenerator.UserControls
{
    public partial class CheckBoxControl : UserControl
    {
        public CheckBoxControl()
        {
            InitializeComponent();
        }

        public object IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(CheckBoxControl));
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(string), typeof(CheckBoxControl));
    }
}
