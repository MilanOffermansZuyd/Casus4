using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
        bool NewPhotoshoot = false;
        PhotoShoot photoshoot = null;

        public AddPhotoshoots(int State, PhotoShoot SelectedPhotoshoot)
        {
            InitializeComponent();
            PopulateListViews();
            if (State == 0)
            {
                NewPhotoshoot = true;
            }
            else if(State == 1)
            {
                NewPhotoshoot = false;
                photoshoot = SelectedPhotoshoot;
                PreSelectItems();
            }
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

            foreach (Concept concept  in Concepts)
            {
                ConceptsListBox.Items.Add(concept.Title);
            }

            foreach (Contract contract in Contracts)
            {
                ContractsListBox.Items.Add(contract.Name);
            }
            foreach (Contact model in Models)
            {
                ModelsListBox.Items.Add(model.FirstName + " " + model.LastName);
            }
        }

        private void PreSelectItems()
        {
            var concepts = ConceptsListBox;
            var contracts = ContractsListBox;
            var models = ModelsListBox;

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PhotoshootPage photoshootPage = new PhotoshootPage();
            photoshootPage.Show();
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //setup data
            string Title = TitleTextBox.Text;
            string Description = DescriptionTextBox.Text;
            Contract contract = null;
            if(ContractsListBox.SelectedItem != null)
            {
                contract = dal.GetContractByTitle(ContractsListBox.SelectedItem.ToString());
            }
            else
            {
                contract = new Contract(0, null,null,false,null);
            }
            //todo: get actual location id
            int Location = 1;

            //grab concepts
            List<Concept> Concepts = new List<Concept>();
            if(ConceptsListBox.SelectedItems != null)
            {
                foreach (string ConceptTitle in ConceptsListBox.SelectedItems)
                {
                    Concept concept = dal.GetConceptByTitle(ConceptTitle);
                    Concepts.Add(concept);
                }
            }
            //setup Photoshoot
            PhotoShoot photoshoot = new PhotoShoot(0, Title, Description, Concepts, contract);


            if (NewPhotoshoot = true)
            {
                //add Photoshoot
                photoshoot.Add(photoshoot);
                //add concept-photshoot link to database
                foreach (Concept concept in photoshoot.Concepts)
                {
                    photoshoot.AddConceptPhotoshoot(concept);
                }

                foreach (string ModelName in ModelsListBox.SelectedItems)
                {
                    Model model = new(null, null, null, null, null, false, true);

                    model = (Model)model.SearchOnName(ModelName);
                    photoshoot.AddPhotoshootModel(model);
                }
            }

            PhotoshootPage photoshootPage = new PhotoshootPage();
            photoshootPage.Show();
            this.Close();
        }
    }
}
