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
    /// Interaction logic for AddPhotoshoots.xaml
    /// </summary>
    public partial class AddPhotoshoots : Window
    {
        public AddPhotoshoots()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PhotoshootPage photoshootPage = new PhotoshootPage();
            photoshootPage.Show();
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PhotoshootPage photoshootPage = new PhotoshootPage();
            photoshootPage.Show();
            this.Close();
        }
    }
}
