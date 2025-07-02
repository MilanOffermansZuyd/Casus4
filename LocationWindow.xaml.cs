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
    /// Interaction logic for LocationWindow.xaml
    /// </summary>
    public partial class LocationWindow : Window
    {
        public LocationWindow()
        {
            InitializeComponent();
        }

        private void BackToMainFromLocation_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void AddLocationFromLocation_Click(object sender, RoutedEventArgs e)
        {
            AddLocationPage addLocationPage = new AddLocationPage();
            addLocationPage.Show();
            this.Close();
        }

        private void DeleteLocationFromLocation_Click(object sender, RoutedEventArgs e)
        {

        } 
        private void EditLocation_Btn(object sender, RoutedEventArgs e)
        {
            AddLocationPage editLocationPage = new AddLocationPage();
            editLocationPage.Show();
            this.Close();
        }
    }
}
