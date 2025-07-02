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
    /// Interaction logic for AddLocationPage.xaml
    /// </summary>
    public partial class AddLocationPage : Window
    {
        public AddLocationPage()
        {
            InitializeComponent();
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            LocationWindow locationWindow = new LocationWindow();
            locationWindow.Show();
            this.Close();
        }

        private void Terug_Click(object sender, RoutedEventArgs e)
        {
            LocationWindow locationWindow= new LocationWindow();
            locationWindow.Show();
            this.Close();
        }
    }
}
