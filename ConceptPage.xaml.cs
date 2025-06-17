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
    /// Interaction logic for Concept.xaml
    /// </summary>
    public partial class ConceptPage : Window
    {
        public ConceptPage()
        {
            InitializeComponent();
        }

        private void BackToMainMenuFromConceptPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void CreateConceptFromConceptPage_Click(object sender, RoutedEventArgs e)
        {
            AddConceptWindow addConceptWindow = new AddConceptWindow();
            addConceptWindow.Show();
            this.Close();
        }
    }
}
