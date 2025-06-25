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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            getAllProject();
        }

        private void getAllProject()
        {
            Project project= new Project();

            ProjectListViewHomePage.ItemsSource = project.Get();
        }

        private void ConceptButtonHomePage_Click(object sender, RoutedEventArgs e)
        {
            AddConceptWindow addConceptWindow = new AddConceptWindow();
            addConceptWindow.Show();
            this.Close();
        }

        private void PhotoshootButton_Click(object sender, RoutedEventArgs e)
        {
            PhotoshootPage photoshootPage = new PhotoshootPage();
            photoshootPage.Show();
            this.Close();
        }

        private void ProjectListViewHomePage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProjectDetailsPage projectDetailsPage = new ProjectDetailsPage();
            projectDetailsPage.Show();
            this.Close();
        }

        private void ConceptButton_Click(object sender, RoutedEventArgs e)
        {
            ConceptPage conceptPage = new ConceptPage();
            conceptPage.Show();
            this.Close();
        }

        private void ContacsButton_Click(object sender, RoutedEventArgs e)
        {
            ModelPage modelPage = new ModelPage(); 
            modelPage.Show();
            this.Close();
        }

        private void NewProjectHomePageButton_Click(object sender, RoutedEventArgs e)
        {
            AddProjectPage addProjectPage = new AddProjectPage();
            addProjectPage.Show();
            this.Close();   
        }
    }
}
