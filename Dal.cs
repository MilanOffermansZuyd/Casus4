using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using Microsoft.Identity.Client;

namespace Casus4
{
    public class DAL
    {
        private readonly string connectionString = "Data Source=LAPTOP-T4RLVBV6;Initial Catalog=IdeaToGocCasus4;Integrated Security=True;Trust Server Certificate=True";
        
        //CRUD for project
        public List<Project> GetAllProjects() 
        {
            var projects = new List<Project>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Project", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(new Project(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDateTime( reader.GetString(3)), null));
                    }
                }
            }

            return projects;
        }
        
        //CRUD for Photoshoot
        public List<PhotoShoot> GetAllPhotoshoots()
        {
            var photoshoots = new List<PhotoShoot>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Photoshoot", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        photoshoots.Add(new PhotoShoot ( reader.GetInt32(0), reader.GetString(1), reader.GetString(2),  null,  null ));
                    }
                }
            }

            return photoshoots;
        }

        public PhotoShoot GetPhotoshootByName(string name)
        {
            PhotoShoot photoshoot = new PhotoShoot(0, null,null,null,null);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Photoshoot WHERE Title = @Title", connection))
            {
                command.Parameters.AddWithValue("@Title", name);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        photoshoot.Id = reader.GetInt32(0);
                        photoshoot.Title = reader.GetString(1);
                        photoshoot.SubTitle = reader.GetString(2);
                    }
                }
                return photoshoot;
            }
        }



        public void AddPhotoshoot(PhotoShoot photoShoot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Photoshoot (Title, Subtitle, Contract, LocationId) VALUES (@Title, @Subtitle, @Contract, @Location)", connection))
            {
                command.Parameters.AddWithValue("@Title", photoShoot.Title);
                command.Parameters.AddWithValue("@Subtitle", photoShoot.SubTitle);
                command.Parameters.AddWithValue("@Contract", photoShoot.Contract.Id);
                command.Parameters.AddWithValue("@Location", 1);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePhotoshoot(PhotoShoot photoshoot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Photoshoot SET Title = @Title, Subtitle = @Subtitle, Contract = @Contract, LocationId = @Location WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", photoshoot.Id);
                command.Parameters.AddWithValue("@Title", photoshoot.Title);
                command.Parameters.AddWithValue("@Subtitle", photoshoot.SubTitle);
                command.Parameters.AddWithValue("@Contract", photoshoot.Contract);
                command.Parameters.AddWithValue("@Location", 1);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeletePhotoshoot(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM Photoshoot WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void AddPhotoshootModel(Model model)
        {
            int PhotoshootId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 Id FROM Photoshoot ORDER BY Id DESC ", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PhotoshootId = reader.GetInt32(0);
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO PhotoshootModels (ContactId, PhotoshootId) VALUES (@Model, @Photoshoot)", connection))
            {
                command.Parameters.AddWithValue("@Model", model.Id);
                command.Parameters.AddWithValue("@Photoshoot", PhotoshootId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void AddPhotoshootExtras(int VolunteerId)
        {
            int PhotoShootId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT Id FROM Photoshoot ORDER BY Id DESC LIMIT 1", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PhotoShootId = reader.GetInt32(0);
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO PhotoshootExtras (ModelId, PhotoshootId) VALUES (@Model, @Photoshoot)", connection))
            {
                command.Parameters.AddWithValue("@Volunteer", VolunteerId);
                command.Parameters.AddWithValue("@Photoshoot", PhotoShootId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        //CRUD For Contracts
        public List<Contract> GetAllContracts()
        {
            var contracts = new List<Contract>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contract", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        byte[] foto = reader["Picture"] as byte[] ?? null;
                        bool isSigned = reader.GetBoolean(3);
                        contracts.Add(new Contract(id, name, foto, isSigned, null));
                    }
                }
            }

            return contracts;
        }
        public Contract GetContractByTitle(string Name)
        {
            Contract contract = new(0, null, null, false, null);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contract WHERE Name = @Name", connection))
            {
                command.Parameters.AddWithValue("@Name", Name);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contract.Id = reader.GetInt32(0);
                        contract.Name = reader.GetString(1);
                        contract.Foto = reader["Picture"] as byte[] ?? null;
                        contract.IsSigned = reader.GetBoolean(3);

                    }
                }
            }
            return contract;
        }

        // ----- CRUD METHODS FOR CONCEPT -----
        public List<Concept> GetAllConcepts()
        {
            var concepts = new List<Concept>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Concept", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        concepts.Add(new Concept(reader.GetInt32(0), reader.GetString(1), reader["LocationId"] as Location ?? null, reader["FotoSketch"] as byte[] ?? null, [reader["FotoResults"] as byte[] ?? null], new Project(0,"Test", "test", DateTime.Now, null), null, null));
                    }
                }
            }

            return concepts;
        }

        public Concept GetConceptByTitle(string Title)
        {
            Concept concept = new();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Concept WHERE Title = @Title", connection))
            {
                command.Parameters.AddWithValue("@Title", Title);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        concept.Id = reader.GetInt32(0);
                        concept.Title = reader.GetString(1);
                        concept.Location = reader["LocationId"] as Location ?? null;
                        concept.FotoSketch = reader["PhotoSketch"] as byte[] ?? null;
                        concept.FotoResult = [reader["PhotoResults"] as byte[] ?? null];
                        concept.Project = new Project(null, "Test", "test", DateTime.Now, null); 
                        concept.Models = null;
                        concept.Extras = null;
                    }
                }
            }
            return concept;
        }


        public void AddConcept(Concept concept)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Concept (Title, LocationId, ProjectId) VALUES (@Title, @LocationId, @ProjectId )",connection))
            {
                command.Parameters.AddWithValue("@Title", concept.Title);
                _ = concept.Location == null ? command.Parameters.AddWithValue("@LocationId", 1) : command.Parameters.AddWithValue("@LocationId", concept.Location.Id);
                command.Parameters.AddWithValue("@ProjectId", concept.Project.Id);


                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateConcept(Concept concept)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Concept SET LocationId = @LocationId, Title = @Title , ProjectId = @ProjectId WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", concept.Id);
                command.Parameters.AddWithValue("@Title", concept.Title);
                _ = concept.Location == null ? command.Parameters.AddWithValue("@LocationId", 1) : command.Parameters.AddWithValue("@LocationId", concept.Location.Id);
                command.Parameters.AddWithValue("@ProjectId", concept.Project.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteConcept(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM Concept WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void AddConceptPhotoshoot(Concept concept)
        {
            int PhotoshootId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 Id FROM Photoshoot ORDER BY Id DESC ", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PhotoshootId = reader.GetInt32(0);
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO ConceptPhotoshoots (ConceptId, PhotoshootId) VALUES (@Concept, @Photoshoot)", connection))
            {
                command.Parameters.AddWithValue("@Concept", concept.Id);
                command.Parameters.AddWithValue("@Photoshoot", PhotoshootId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        // ----- CRUD METHODS FOR CONTACT -----
        public List<Contact> GetAllContacts()
        {
            var contacts = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contact", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contacts.Add(new Model(System.Convert.ToInt32(reader["Id"]), reader.GetString(1), reader.GetString(2), (byte[])reader["picture"], reader.GetInt32(4), (Location)reader["location"], reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetBoolean(9), reader.GetBoolean(10)));
                    }
                }
            }

            return contacts;
        }


        //public Model FindModelByName(string name)
        //{
            
        //    string[] Name = name.Split(' ');

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand("SELECT * FROM Contact WHERE FirstName = @First AND LastName = @Last", connection))
        //    {
        //        connection.Open();
        //        command.Parameters.AddWithValue("@First", Name[0]);
        //        command.Parameters.AddWithValue("@Last", Name[1]);
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                int Id = reader.GetInt32(0);
        //                string FirstName = reader.GetString(1);
        //                string LastName = reader.GetString(2);
        //                byte[] Picture = (byte[])reader["Picture"];
        //                Location location = GetLocationById(reader.GetInt32(4));

        //                Model model = new(Id, FirstName, LastName, Picture, location, false, true);

        //                return model;
        //            }
        //        }
        //    }
        //    throw new Exception(nameof(FindModelByName));
        //}

        //public Contact FindContacts(int id)
        //{

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand("SELECT * FROM Contact WHERE Id = @Id", connection))
        //    {
        //        connection.Open();
        //        command.Parameters.AddWithValue("@Id", id);
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                return new Helper(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), (byte[])reader["Picture"], (Location)reader["LocationId"]);
        //            }
        //        }
        //    }
        //    throw new Exception(nameof(FindContacts));
        //}



        public void AddContact(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Contact (FirstName, LastName, Picture, Location, Description, ExtraInformation, Naked, Rol) VALUES (@FirstName, @LastName, @Picture, @Location, @Description, @ExtraInformation, @Naked, @Rol)", connection))
            {
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description", contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", contact.Naked);
                command.Parameters.AddWithValue("@Rol", contact.Rol);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Contact SET FirstName = @FirstName, LastName = @LastName, Picture = @Picture, Location = @Location, Description = @Description, ExtraInformation = @ExtraInformation, Naked = @Naked, Rol = @Rol, WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", contact.Id);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description",contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", contact.Naked);
                command.Parameters.AddWithValue("@Rol", contact.Rol);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteContact(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM Contact WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal void UpdateConcept()
        {
            throw new NotImplementedException();
        }

        // CRUD for Location
        public Location GetLocationById(int Id)
        {
            Location location = new Location(Id, null, null, null, null, null);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Location WHERE Id = @Id", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", Id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        location.Street = reader.GetString(1);
                        location.HouseNumber = reader.GetString(2);
                        location.PostalCode = reader.GetString(3);
                        location.City = reader.GetString(4);
                        location.Country = reader.GetString(5);
                    }
                    return location;
                }
            }
            throw new Exception(nameof(GetLocationById));
        }

    }
}
