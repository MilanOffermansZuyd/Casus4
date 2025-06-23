using Microsoft.Data.SqlClient;
using System;

namespace Casus4
{
    public class DAL
    {
        private readonly string connectionString = "Data Source=LAPTOP-T4RLVBV6;Initial Catalog=IdeaToGoCasus4;Integrated Security=True;Trust Server Certificate=True";
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

        public void AddPhotoshoot(PhotoShoot photoShoot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Photoshoot (Title, Subtitle, FotoSketch, FotoResults, Contract, Location) VALUES (@Title, @Subtitle, @FotoSketch, @FotoResults, @Contract, @Location)", connection))
            {
                command.Parameters.AddWithValue("@Title", photoShoot.Title);
                command.Parameters.AddWithValue("@Subtitle", photoShoot.SubTitle);
                command.Parameters.AddWithValue("@Contract", photoShoot.Contracts);
                command.Parameters.AddWithValue("@Location", photoShoot.Concepts);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePhotoshoot(PhotoShoot photoshoot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Photoshoot SET Title = @Title, Subtitle = @Subtitle, FotoSketch = @FotoSketch, FotoResults = @FotoResults, Contract = @Contract, Location = @Location WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", photoshoot.Id);
                command.Parameters.AddWithValue("@Title", photoshoot.Title);
                command.Parameters.AddWithValue("@Subtitle", photoshoot.Title);
                command.Parameters.AddWithValue("@Contract", photoshoot.Contracts);
                command.Parameters.AddWithValue("@Location", photoshoot.Concepts);

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
        public void AddPhotoshootModels(int ModelId)
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
            using (SqlCommand command = new SqlCommand("INSERT INTO PhotoshootModels (ModelId, PhotoshootId) VALUES (@Model, @Photoshoot)", connection))
            {
                command.Parameters.AddWithValue("@Model", ModelId);
                command.Parameters.AddWithValue("@Photoshoot", PhotoShootId);

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

                       string name = reader.GetString(0);
                       byte[] foto = reader["Picture"] as byte[] ?? null;
                       bool isSigned = reader.GetBoolean(2);
                        contracts.Add(new Contract(name, foto, isSigned, null));
                    }
                }
            }

            return contracts;
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
                        concepts.Add(new Concept(reader.GetInt32(0), reader.GetString(1), reader["LocationId"] as Location ?? null, reader["FotoSketch"] as byte[] ?? null, [reader["FotoResults"] as byte[] ?? null], new Project("Test", "test", DateTime.Now, null), null, null));
                    }
                }
            }

            return concepts;
        }

        public void AddConcept(Concept concept)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Concept (Location, Title) VALUES (@Location, @Title)", connection))
            {
                command.Parameters.AddWithValue("@Location", concept.Location);
                command.Parameters.AddWithValue("@Title", concept.Title);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateConcept(Concept concept)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Concept SET Location = @Location, Title = @Title WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", concept.Id);
                command.Parameters.AddWithValue("@Location", concept.Location);
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
        public void AddConceptPhotoshoot(int ConceptId)
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
            using (SqlCommand command = new SqlCommand("INSERT INTO ConceptPhotoshoots (ConceptId, PhotoshootId) VALUES (@Concept, @Photoshoot)", connection))
            {
                command.Parameters.AddWithValue("@Concept", ConceptId);
                command.Parameters.AddWithValue("@Photoshoot", PhotoShootId);

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
                        contacts.Add(new Helper ( reader.GetInt32(0), reader.GetString(1),reader.GetString(2), (byte[])reader["Picture"], (Location)reader["LocationId"] ));
                    }
                }
            }

            return contacts;
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
    }
}
