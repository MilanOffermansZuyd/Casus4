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
    /// Interaction logic for AddPropsPage.xaml
    /// </summary>
    public partial class AddPropsPage : Window
    {
        public AddPropsPage()
        {
            InitializeComponent();
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            PropsPage propsPage = new PropsPage();
            propsPage.Show();
            this.Close();
        }
    }
}
