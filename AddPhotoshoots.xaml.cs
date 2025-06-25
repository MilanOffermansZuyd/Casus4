using System;
using System.Collections.Generic;
using System.Configuration;
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
        DAL dal = new DAL();
        public AddPhotoshoots()
        {
            InitializeComponent();
            PopulateListViews();
        }

        private void PopulateListViews()
        {
            ConceptsListBox.Items.Clear();
            ContractsListBox.Items.Clear();
            ModelsListBox.Items.Clear();
            VolunteersListBox.Items.Clear();

            List<Concept> Concepts = dal.GetAllConcepts();
            List<Contract> Contracts = dal.GetAllContracts();
            List<Contact> Models = dal.GetAllContacts();
            ConceptsListBox.ItemsSource = Concepts;
            ContractsListBox.ItemsSource = Contracts;
            ModelsListBox.ItemsSource = Models;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PhotoshootPage photoshootPage = new PhotoshootPage();
            photoshootPage.Show();
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string Title = TitleTextBox.Text;
            string Description = DescriptionTextBox.Text;
            int Contract = ContractsListBox.SelectedIndex;
            int Location = 1;

            var photoschoot = new PhotoShoot(0, Title, Description, null, null);
            dal.AddPhotoshoot(photoschoot);
            foreach (var ConceptId in ConceptsListBox.SelectedItems)
            {
                dal.AddConceptPhotoshoot((int)ConceptId);
            }
            //foreach (var ContractId in ConceptsListBox.SelectedItems)
            //{
            //    dal.AddConceptPhotoshoot((int)ContractId);
            //}
            foreach (var ModelId in ConceptsListBox.SelectedItems)
            {
                dal.AddPhotoshootModels((int)ModelId);
            }
            foreach (var VolunteerId in ConceptsListBox.SelectedItems)
            {
                dal.AddPhotoshootExtras((int)VolunteerId);
            }
            PhotoshootPage photoshootPage = new PhotoshootPage();
            photoshootPage.Show();
            this.Close();
        }
    }
}
