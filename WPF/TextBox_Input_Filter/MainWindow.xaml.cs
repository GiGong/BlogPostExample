using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace TextBox_Input_Filter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FILTER_STRING = "GIGONG";
        private readonly string[] ARROW_KEY_STRINGS = { "Left", "Right", "Up", "Down" };
        private readonly HashSet<string> inputFilter = new HashSet<string>();

        public MainWindow()
        {
            InitializeComponent();

            foreach (char item in FILTER_STRING)
            {
                inputFilter.Add(item.ToString());
            }
            inputFilter.UnionWith(ARROW_KEY_STRINGS);
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

        private void textBoxCharacter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!((Key.A <= e.Key && e.Key <= Key.Z)
                || e.Key == Key.Back))
            {
                e.Handled = true;
            }
        }

        private void textBoxGIGONG_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(inputFilter.Contains(e.Key.ToString())
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
