using System.Windows;

namespace Casus4
{
    /// <summary>
    /// Interaction logic for Photoshoot.xaml
    /// </summary>
    public partial class PhotoshootPage : Window
    {
        DAL dal = new DAL();
        public PhotoshootPage()
        {
            InitializeComponent();
            PhotoshootDataGrid.ItemsSource = dal.GetAllPhotoshoots();
        }

        private void AddPhotoshoot_Click(object sender, RoutedEventArgs e)
        {
            AddPhotoshoots addPhotoshoots = new AddPhotoshoots(0, 0);
            addPhotoshoots.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void PhotoshootDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            AddPhotoshoots addPhotoshoots = new AddPhotoshoots(1, 0);
            addPhotoshoots.Show();
            this.Close();
        }
    }
}
