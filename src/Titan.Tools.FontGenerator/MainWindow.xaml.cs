using System.Windows;
using System.Windows.Input;

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
    }
}
