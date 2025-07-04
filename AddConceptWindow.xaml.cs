﻿using System.Windows;
using System.Windows.Media.Imaging;

namespace Casus4
{
    /// <summary>
    /// Interaction logic for AddConceptWindow.xaml
    /// </summary>
    public partial class AddConceptWindow : Window
    {
        public AddConceptWindow()
        {
            InitializeComponent();
            AddProjectToCombox();
        }

        Project project = new Project();
        byte[] sketchToBeSaved;

        private void AddProjectToCombox()
        {

            var projects = project.Get();

            foreach ( var item in projects ) 
            {
                ComboBoxProject.Items.Add(item.Title);
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
                    SketchAfbeelding.Source = bitmap;
                    sketchToBeSaved = ConceptImageHelper.ImageSourceToByteArray(bitmap);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kon afbeelding niet laden: " + ex.Message);
                }
            }
        }

        private void BackToConceptPageFromCreate_Click(object sender, RoutedEventArgs e)
        {
            ConceptPage conceptPage = new ConceptPage();
            conceptPage.Show();
            this.Close();
        }

        private void SaveAddConcept_Click(object sender, RoutedEventArgs e)
        {
            var projects = project.Get();
            foreach (var item in projects)
            {
                if (ComboBoxProject.SelectedItem.ToString() == item.Title)
                {
                    var title = TextTitle.Text;

                    Concept concept = new Concept(null, title, null, sketchToBeSaved, null, item, null, null, Description_TextBox.Text);

                    concept.Add(concept);

                    ConceptPage conceptPage = new ConceptPage();
                    conceptPage.Show();
                    this.Close();
                }
            }
        }

        private void CreateNewProp_Click(object sender, RoutedEventArgs e)
        {
            Prop prop = new Prop("NAME","description");
            prop.Add(prop);
        }

        private void CreateNewLocation_Click(object sender, RoutedEventArgs e)
        {
            Location location = new Location(null, "name", new LocalAuthority(null, "test"), new Adress(null, "street", "house", "6445", "city"), new Country(null, "name") );
           location.add(location);
        }

        private void CreateNewModel_Click(object sender, RoutedEventArgs e)
        {
            Model model = new Model( null, "test", "last", [], new Location(), "yes", "ee",false , false);
            model.Add(model);
        }
    }
}
