using System.Windows;

namespace Casus4
{
    /// <summary>
    /// Interaction logic for Photoshoot.xaml
    /// </summary>
    public partial class PhotoshootPage : Window
    {
        public PhotoshootPage()
        {
            InitializeComponent();
        }

        private void AddPhotoshoot_Click(object sender, RoutedEventArgs e)
        {
            AddPhotoshoots addPhotoshoots = new AddPhotoshoots();
            addPhotoshoots.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
