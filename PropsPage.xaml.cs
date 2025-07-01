using System.Windows;

namespace Casus4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PropsPage : Window
    {
        public PropsPage()
        {
            InitializeComponent();
        }

        private void BtnAddProp_Click(object sender, RoutedEventArgs e)
        {
            AddPropsPage addPropsPage = new AddPropsPage();
            addPropsPage.Show();
            this.Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
            this.Close();
        }
    }
}
