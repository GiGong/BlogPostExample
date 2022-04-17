using System.Windows;
using System.Windows.Input;

namespace TextBox_Input_Filter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtCharUpper_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!((Key.A <= e.Key && e.Key <= Key.Z && Keyboard.IsKeyDown(Key.LeftShift))
                || e.Key == Key.Back))
            {
                e.Handled = true;
            }

        }

        private void textBoxNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!((Key.D0 <= e.Key && e.Key <= Key.D9)
                || (Key.NumPad0 <= e.Key && e.Key <= Key.NumPad9)
                || e.Key == Key.Back))
            {
                e.Handled = true;
            }
        }

        private void textBoxNumberHyphen_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!((Key.D0 <= e.Key && e.Key <= Key.D9)
                || (Key.NumPad0 <= e.Key && e.Key <= Key.NumPad9)
                || e.Key == Key.OemMinus
                || e.Key == Key.Back))
            {
                e.Handled = true;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtShow.Text = e.Key.ToString();
        }
    }
}
