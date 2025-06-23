using System.Windows;

namespace Casus4
{
    /// <summary>
    /// Interaction logic for Concept.xaml
    /// </summary>
    public partial class ConceptPage : Window
    {
        private Concept Concept = new Concept();
        public ConceptPage()
        {
            InitializeComponent();
            initializeConcept();
        }

        private void initializeConcept()
        {
            var concepts = Concept.get();
            ConceptPageListView.ItemsSource = concepts;
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
