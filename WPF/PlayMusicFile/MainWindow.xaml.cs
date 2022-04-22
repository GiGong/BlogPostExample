using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;

namespace PlayMusicFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MediaPlayer player = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == true)
            {
                player.Open(new Uri(file.FileName));
                lblFileName.Content = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            lblMusicPosition.Content = player.Position.ToString(@"mm\:ss");
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            player.Close();
        }
    }
}
