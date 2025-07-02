using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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
        PhotoShoot photoshoot = new PhotoShoot(0, new DateTime(2000, 1, 1), null, null, null, null, null);

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
            PropsListBox.Items.Clear();

            List<Concept> Concepts = dal.GetAllConcepts();
            List<Contract> Contracts = dal.GetAllContracts();
            List<Contact> Models = dal.GetAllModels();
            List<Prop> Props = dal.GetAllProps();

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
            foreach (Prop prop in Props)
            {
                PropsListBox.Items.Add(prop.Name);
            }
        }

        private void PreSelectItems()
        {
            var concepts = ConceptsListBox;
            var contracts = ContractsListBox;
            var models = ModelsListBox;
            var props = PropsListBox;

            //Contract contract = dal.GetContractById(photoshoot.Contracts.contract.Id);

            //foreach (var item in ContractsListBox.Items)
            //{
            //    if(item == contract.Name)
            //    {
            //        ContractsListBox.SelectedItems.Add(item);
            //    }
            //}
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

            //concepts
            List<Concept> concepts = new List<Concept>();
            if (ConceptsListBox.SelectedItems != null)
            {
                foreach (string ConceptTitle in ConceptsListBox.SelectedItems)
                {
                    Concept concept = dal.GetConceptByTitle(ConceptTitle);
                    concepts.Add(concept);
                }
            }
            //models
            List<Model> models = new List<Model>();

            if (ModelsListBox.SelectedItems != null)
            {
                foreach (string modelString in ModelsListBox.SelectedItems)
                {
                    Model model = null;

                    //model = dal.GetModelByName(modelString);

                    models.Add(model);
                }

            }

            //contracts
            List<Contract> contracts = new List<Contract>();

            if (ContractsListBox.SelectedItems != null)
            {
                foreach (string contractString in ContractsListBox.SelectedItems)
                {
                    Contract contract = null;

                    contract = dal.GetContractByTitle(contractString);

                    contracts.Add(contract);
                }

            }

            //props
            List<Prop> props = new List<Prop >();

            if (PropsListBox.SelectedItems != null)
            {
                foreach (string propString in PropsListBox.SelectedItems)
                {

                    Prop prop = null;

                    prop = dal.GetPropByName(propString);

                    props.Add(prop);
                }

            }


            //date
            DateTime date = (DateTime)DatePicker.SelectedDate;
            //location
            string locationString = LocationsListBox.SelectedItem.ToString();
            Location location = dal.GetLocationByString(locationString);

            //setup Photoshoot
            PhotoShoot photoshoot = new PhotoShoot(0, date, location, concepts, contracts, models, props);


            if (NewPhotoshoot = true)
            {
                photoshoot.Add();
            }

            if(NewPhotoshoot = false)
            {
                photoshoot.Date = date;
                photoshoot.Location = location;
                photoshoot.Concepts = concepts;
                photoshoot.Models = models;
                photoshoot.Contracts = contracts;
                photoshoot.Props = props;

                photoshoot.Edit();
            }

            PhotoshootPage photoshootPage = new PhotoshootPage();
            photoshootPage.Show();
            this.Close();
        }
    }
}
