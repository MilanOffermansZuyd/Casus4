using System.Windows;
using System.Windows.Controls;

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
            AddPhotoshoots addPhotoshoots = new AddPhotoshoots(0, null);
            addPhotoshoots.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void PhotoshootDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            object item = PhotoshootDataGrid.SelectedItem;
            string name = (PhotoshootDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

            PhotoShoot photoshoot = dal.GetPhotoshootByName(name);

            AddPhotoshoots addPhotoshoots = new AddPhotoshoots(1, photoshoot);
            addPhotoshoots.Show();
            this.Close();
        }
    }
}
