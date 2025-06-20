using Microsoft.Data.SqlClient;

namespace Casus4
{
    public class DAL
    {
        private readonly string connectionString = "Data Source=LAPTOP-T4RLVBV6;Initial Catalog=IdeaToGoCasus4;Integrated Security=True;Trust Server Certificate=True";

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
                        photoshoots.Add(new PhotoShoot
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Subtitle = reader.GetString(2),
                            FotoSketch = reader.GetString(3),
                            FotoResults = reader.GetString(4),
                            Contract = reader.GetInt32(5),
                            Location = reader.GetString(6)
                        });
                    }
                }
            }

            return photoshoots;
        }

        public void AddPhotoshoot(PhotoShoot photoshoot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Photoshoot (Title, Subtitle, FotoSketch, FotoResults, Contract, Location) VALUES (@Title, @Subtitle, @FotoSketch, @FotoResults, @Contract, @Location)", connection))
            {
                command.Parameters.AddWithValue("@Title", photoshoot.Title);
                command.Parameters.AddWithValue("@Subtitle", photoshoot.Subtitle);
                command.Parameters.AddWithValue("@FotoSketch", photoshoot.FotoSketch);
                command.Parameters.AddWithValue("@FotoResults", photoshoot.FotoResults);
                command.Parameters.AddWithValue("@Contract", photoshoot.Contract);
                command.Parameters.AddWithValue("@Location", photoshoot.Location);

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
                command.Parameters.AddWithValue("@Subtitle", photoshoot.Subtitle);
                command.Parameters.AddWithValue("@FotoSketch", photoshoot.FotoSketch);
                command.Parameters.AddWithValue("@FotoResults", photoshoot.FotoResults);
                command.Parameters.AddWithValue("@Contract", photoshoot.Contract);
                command.Parameters.AddWithValue("@Location", photoshoot.Location);

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
                        concepts.Add(new Concept
                        {
                            Id = reader.GetInt32(0),
                            Location = reader.GetString(1),
                            Title = reader.GetString(2)
                        });
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
                        contacts.Add(new Contact
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Picture = reader.GetString(3),
                            Location = reader.GetString(4)
                        });
                    }
                }
            }

            return contacts;
        }

        public void AddContact(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Contact (FirstName, LastName, Picture, Location) VALUES (@FirstName, @LastName, @Picture, @Location)", connection))
            {
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);

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
                command.Parameters.AddWithValue("@Location", contact.Location);

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
