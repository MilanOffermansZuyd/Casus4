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
            PopulateDataGrid();
        }

        public void PopulateDataGrid()
        {
            List<PhotoShoot> photoshoots = new List<PhotoShoot>();
            foreach (PhotoShoot photoshoot in photoshoots){
                PhotoshootDataGrid.Items.Add(photoshoot);
            }
            PhotoshootDataGrid.Columns[0].Visibility = Visibility.Collapsed;
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
            string Id = ((TextBlock)PhotoshootDataGrid.SelectedCells[0].Column.GetCellContent(item)).Text;

            int id = Int32.Parse(Id);

            PhotoShoot photoshoot = dal.GetPhotoshootById(id);

            AddPhotoshoots addPhotoshoots = new AddPhotoshoots(1, photoshoot);
            addPhotoshoots.Show();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PhotoshootDataGrid.SelectedItem != null)
            {
                object item = PhotoshootDataGrid.SelectedItem;
                string Id = ((TextBlock)PhotoshootDataGrid.SelectedCells[0].Column.GetCellContent(item)).Text;
                int id = Int32.Parse(Id);
                PhotoShoot photoshoot = dal.GetPhotoshootById(id);
                photoshoot.Remove();
                string message = """"
                Photoshoot Deleted
                """";
                string title = "Task completed";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBox.Show(message, title, buttons);
            }
            else{
                string message = """"
                No PhotoShoot Selected
                """";
                string title = "Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBox.Show(message, title, buttons);
            }
            

        }
    }
}
