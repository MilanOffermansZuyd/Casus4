using Microsoft.Data.SqlClient;
using System.IO;
using System.Security.Cryptography.Xml;
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
        private PhotoShoot  PhotoShoot = new PhotoShoot();

        private byte[] sketchImage;

        private List<BitmapImage> _images = new List<BitmapImage>();
        private int _currentIndex = 0;

        private List<byte[]> _imageBytesList = new List<byte[]>();

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
            TextUpdateLocatie.Text = concept.Location?.Name?? "leeg";
            TextUpdateModel.Text = concept?.Models?.ToString() ?? "leeg";
            ComboBoxProjectUpdate.Text = concept.Project.Title;
            ComboBoxProjectUpdate.SelectedItem = concept.Project.Title;
            Description_TextBox.Text = concept.Description;
            foreach (var item in PhotoShoot.Get())
            {
                FotoshootsGridView.Items.Add(item);
            }

            if (concept.FotoSketch != null)
            {
                SketchAfbeelding.Source = ByteArrayToBitmapImage(concept.FotoSketch);
                sketchImage = concept.FotoSketch;
            }
            else 
            {
                SketchAfbeelding.Source= new BitmapImage(new Uri("pack://application:,,,/Images/default.png"));
            }
            if(concept.FotoResult != null)
            {
                _images.Clear();
                foreach (var bytes in concept.FotoResult)
                {
                    if (bytes != null)
                    {
                        var bmp = ByteArrayToBitmapImage(bytes);
                        _images.Add(bmp);
                        _imageBytesList.Add(bytes);
                    }
                }
            }


            if (_images.Any())
            {
                _currentIndex = 0;
                FotoAfbeelding.Source = _images[_currentIndex];
            }
            else
            {
                FotoAfbeelding.Source = new BitmapImage(new Uri("pack://application:,,,/Images/default.png"));
            }
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

        private void UploadSketchImage_Click(object sender, RoutedEventArgs e)
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
                    sketchImage = ConceptImageHelper.ImageSourceToByteArray(bitmap);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kon afbeelding niet laden: " + ex.Message);
                }
            }
        }

        private void UploadResultImages_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = true,
                Filter = "Afbeeldingen (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _images.Clear();
                    _imageBytesList.Clear();

                    foreach (string filePath in openFileDialog.FileNames)
                    {
                        var bitmap = new BitmapImage(new Uri(filePath));
                        _images.Add(bitmap);

                        byte[] bytes = ConceptImageHelper.ImageSourceToByteArray(bitmap);
                        _imageBytesList.Add(bytes);
                    }

                    if (_images.Any())
                    {
                        _currentIndex = 0;
                        FotoAfbeelding.Source = _images[_currentIndex];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kon afbeeldingen niet laden: " + ex.Message);
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


                    Concept concept = new Concept(Concept.Id, title, null, sketchImage ?? null, _imageBytesList ?? null, item, null, null ,Description_TextBox.Text);

                    concept.Edit(concept);

                    ConceptPage conceptPage = new ConceptPage();
                    conceptPage.Show();
                    this.Close();
                }
            }
        }

        private void VolgendeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_images.Count == 0) return;

            _currentIndex++;
            if (_currentIndex >= _images.Count)
                _currentIndex = 0;

            FotoAfbeelding.Source = _images[_currentIndex];
        }

        private void VorigeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_images.Count == 0) return;

            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = _images.Count - 1;

            FotoAfbeelding.Source = _images[_currentIndex];
        }

        public BitmapImage ByteArrayToBitmapImage(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }
    }
}
