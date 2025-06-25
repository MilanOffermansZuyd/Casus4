using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Casus4
{
    /// <summary>
    /// Interaction logic for UpdateConceptWindow.xaml
    /// </summary>
    public partial class UpdateConceptWindow : Window
    {
        private Concept Concept = new Concept();
        public UpdateConceptWindow(Concept concept)
        {
            InitializeComponent();
            AddProjectToCombox();
            Concept = concept;
            LoadDataConcept(Concept);
        }

        private void LoadDataConcept(Concept concept)
        {
            TextUpdateTitle.Text = concept.Title;
            TextUpdateLocatie.Text = concept.Location?.City ?? "leeg";
            TextUpdateModel.Text = concept?.Models?.ToString() ?? "leeg";
            ComboBoxProjectUpdate.SelectedItem = concept.Project.Title;

        }

        Project project = new Project();

        private void AddProjectToCombox()
        {

            var projects = project.Get();

            foreach ( var item in projects ) 
            {
                ComboBoxProjectUpdate.Items.Add(item.Title);
            }
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Afbeeldingen (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var uri = new Uri(openFileDialog.FileName);
                    var bitmap = new BitmapImage(uri);
                    SketchAfbeeldingUpdateConcept.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kon afbeelding niet laden: " + ex.Message);
                }
            }
        }

        private void BackToConceptPageFromUpdate_Click(object sender, RoutedEventArgs e)
        {
            ConceptPage conceptPage = new ConceptPage();
            conceptPage.Show();
            this.Close();
        }

        private void UpdateAddConcept_Click(object sender, RoutedEventArgs e)
        {
            var projects = project.Get();
            foreach (var item in projects)
            {
                if (ComboBoxProjectUpdate.SelectedItem.ToString() == item.Title)
                {
                    var title = TextUpdateTitle.Text;


                    Concept concept = new Concept(Concept.Id, title, null, null, null, item, null, null);

                    concept.Edit(concept);

                    ConceptPage conceptPage = new ConceptPage();
                    conceptPage.Show();
                    this.Close();
                }
            }
        }
    }
}
