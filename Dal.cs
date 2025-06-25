using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;

namespace Casus4
{
    public class DAL
    {
        private readonly string connectionString = "Data Source=asuskyle;Initial Catalog=IdeaToGoCasus4;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
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
        public Contract GetContractById(int Id)
        {
            Contract contract = new(0, null, null, false, null);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contract WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", Id);

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
                        concepts.Add(new Concept(reader.GetInt32(0), reader.GetString(1), reader["LocationId"] as Location ?? null, reader["PhotoSketch"] as byte[] ?? null, [reader["PhotoResults"] as byte[] ?? null], new Project("Test", "test", DateTime.Now, null), null, null));
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
                        concept.Project = new Project("Test", "test", DateTime.Now, null);
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
            using (SqlCommand command = new SqlCommand("INSERT INTO Concept (LocationId, Title) VALUES (@Location, @Title)", connection))
            {
                command.Parameters.AddWithValue("@Location", concept.Location.Id);
                command.Parameters.AddWithValue("@Title", concept.Title);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateConcept(Concept concept)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Concept SET LocationId = @Location, Title = @Title WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", concept.Id);
                command.Parameters.AddWithValue("@Location", concept.Location.Id);
                command.Parameters.AddWithValue("@Title", concept.Title);

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
                        int Id = reader.GetInt32(0);
                        string FirstName = reader.GetString(1);
                        string LastName = reader.GetString(2);
                        byte[] Picture = (byte[])reader["Picture"];
                        Location location = GetLocationById(reader.GetInt32(4));

                        contacts.Add(new Helper ( Id, FirstName, LastName, Picture, location));
                    }
                }
            }

            return contacts;
        }


        public Model FindModelByName(string name)
        {
            
            string[] Name = name.Split(' ');

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contact WHERE FirstName = @First AND LastName = @Last", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@First", Name[0]);
                command.Parameters.AddWithValue("@Last", Name[1]);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int Id = reader.GetInt32(0);
                        string FirstName = reader.GetString(1);
                        string LastName = reader.GetString(2);
                        byte[] Picture = (byte[])reader["Picture"];
                        Location location = GetLocationById(reader.GetInt32(4));

                        Model model = new(Id, FirstName, LastName, Picture, location, false, true);

                        return model;
                    }
                }
            }
            throw new Exception(nameof(FindModelByName));
        }

        public Contact FindContacts(int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contact WHERE Id = @Id", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Helper(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), (byte[])reader["Picture"], (Location)reader["LocationId"]);
                    }
                }
            }
            throw new Exception(nameof(FindContacts));
        }



        public void AddContact(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Contact (FirstName, LastName, Picture, Location) VALUES (@FirstName, @LastName, @Picture, @Location)", connection))
            {
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Contact SET FirstName = @FirstName, LastName = @LastName, Picture = @Picture, Location = @Location WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", contact.Id);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location.Id);

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
