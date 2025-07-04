using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Casus4
{
    /// <summary>
    /// Interaction logic for Model.xaml
    /// </summary>
    public partial class ModelPage : Window
    {
        DAL dal = new DAL();
        public ModelPage()
        {
            InitializeComponent();

            var contacts = dal.GettAllContacts();

            ModellenListViewModelPage.ItemsSource = contacts;
        }

        private void CreateNewModelFromModelPage_Click(object sender, RoutedEventArgs e)
        {
            AddModelWindow addModelWindow = new AddModelWindow();
            addModelWindow.Show();
            this.Close();
        }

        private void BackToHomePageFromModelsPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ModellenListViewModelPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
