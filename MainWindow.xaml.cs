using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Casus4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnConcept_Click(object sender, RoutedEventArgs e)
    {
        ConceptPage conceptPage = new ConceptPage();
        conceptPage.Show();
        this.Close();
    }

    private void BtnModel_Click(object sender, RoutedEventArgs e)
    {
        ModelPage modelPage = new ModelPage();
        modelPage.Show();
        this.Close();
    }

    private void BtnProject_Click(object sender, RoutedEventArgs e)
    {
        ProjectPage projectPage = new ProjectPage();
        projectPage.Show();
        this.Close();
    }

    private void BtnPhotoshoot_Click(object sender, RoutedEventArgs e)
    {
        PhotoshootPage photoshootPage = new PhotoshootPage();
        photoshootPage.Show();
        this.Close();
    }
}