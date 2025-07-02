using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using Microsoft.Identity.Client;

namespace Casus4
{
    public class DAL
    {
        private readonly string connectionString = "Data Source=JReuleaux;Initial Catalog=IdeaToGoCasus4;Integrated Security=True;Trust Server Certificate=True";
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
                        projects.Add(new Project(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDateTime(reader.GetString(3)), null));
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
                        photoshoots.Add(new PhotoShoot(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), null, null));
                    }
                }
            }

            return photoshoots;
        }

        public PhotoShoot GetPhotoshootByName(string name)
        {
            PhotoShoot photoshoot = new PhotoShoot(0, null, null, null, null);

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
                        concepts.Add(new Concept(reader.GetInt32(0), reader.GetString(1), reader["LocationId"] as Location ?? null, reader["FotoSketch"] as byte[] ?? null, [reader["FotoResults"] as byte[] ?? null], new Project(0, "Test", "test", DateTime.Now, null), null, null));
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
            using (SqlCommand command = new SqlCommand("INSERT INTO Concept (Title, LocationId, ProjectId) VALUES (@Title, @LocationId, @ProjectId )", connection))
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
        public List<Contact> GetAllModels()
        {
            var contacts = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contact WHERE Naked IS NOT NULL", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int? id = reader["Id"] as int?;
                        string firstName = reader["FirstName"].ToString();
                        string lastName = reader["LastName"].ToString();
                        byte[] picture = reader["Picture"] as byte[];
                        int locationId = (int)reader["LocationId"];
                        Location location = GetLocationById(locationId);
                        string description = reader["Description"].ToString();
                        string extraInformation = reader["ExtraInfo"].ToString();
                        bool naked = (bool)reader["Naked"];
                        contacts.Add(new Model(id, firstName, lastName, picture, location, description, extraInformation, naked, null, null));
                    }
                }
            }
            return contacts;
        }

        public List<Contact> GetAllMakeUpArtists()
        {
            var contacts = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contact WHERE GetsResourcesPaid IS NOT NULL", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int? id = reader["Id"] as int?;
                        string firstName = reader["FirstName"].ToString();
                        string lastName = reader["LastName"].ToString();
                        byte[] picture = reader["Picture"] as byte[];
                        int locationId = (int)reader["LocationId"];
                        Location location = GetLocationById(locationId);
                        string description = reader["Description"].ToString();
                        string extraInformation = reader["ExtraInfo"].ToString();
                        bool getsResourcesPaid = (bool)reader["GetsResourcesPaid"];
                        contacts.Add(new MakeUpArtist(id, firstName, lastName, picture, location, description, extraInformation, null, null, getsResourcesPaid));
                    }
                }
            }
            return contacts;
        }

        public List<Contact> GetAllHelpers()
        {
            var contacts = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contact WHERE GetsPaid IS NOT NULL", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int? id = reader["Id"] as int?;
                        string firstName = reader["FirstName"].ToString();
                        string lastName = reader["LastName"].ToString();
                        byte[] picture = reader["Picture"] as byte[];
                        int locationId = (int)reader["LocationId"];
                        Location location = GetLocationById(locationId);
                        string description = reader["Description"].ToString();
                        string extraInformation = reader["ExtraInfo"].ToString();
                        bool getsPaid = (bool)reader["GetsPaid"];
                        contacts.Add(new Helper(id, firstName, lastName, picture, location, description, extraInformation, null, getsPaid, null));
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
                        string description = reader.GetString(5);
                        string extraInformation = reader.GetString(6);
                        bool naked = (bool)reader["@Naked"];

                        Model model = new Model(Id, FirstName, LastName, Picture, location, description, extraInformation, naked, null, null);

                        return model;
                    }
                }
            }
            throw new Exception(nameof(FindModelByName));
        }

        public Contact FindModel(int id)
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
                        return new Model(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), (byte[])reader["Picture"], (Location)reader["Location"], reader.GetString(3), reader.GetString(4), (bool)reader["Naked"], (bool)reader["GetsPayed"], (bool)reader["GetsResourcesPayed"]);
                    }
                }
            }
            throw new Exception(nameof(FindModel));
        }

        public Contact FindMakeUpArtist(int id)
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
                        return new MakeUpArtist(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), (byte[])reader["Picture"], (Location)reader["Location"], reader.GetString(3), reader.GetString(4), (bool)reader["Naked"], (bool)reader["GetsPayed"], (bool)reader["GetsResourcesPayed"]);
                    }
                }
            }
            throw new Exception(nameof(FindMakeUpArtist));
        }

        public Contact FindHelper(int id)
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
                        return new Helper(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), (byte[])reader["Picture"], (Location)reader["Location"], reader.GetString(3), reader.GetString(4), (bool)reader["Naked"], (bool)reader["GetsPayed"], (bool)reader["GetsResourcesPayed"]);
                    }
                }
            }
            throw new Exception(nameof(FindHelper));
        }



        public void AddModel(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Contact (FirstName, LastName, Picture, Location, Description, ExtraInformation, Naked, GetsPaid, GetsResourcesPaid) VALUES (@FirstName, @LastName, @Picture, @Location, @Description, @ExtraInformation, @Naked, GetsPaid, GetsResourcesPaid)", connection))
            {
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description", contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", contact.Naked);
                command.Parameters.AddWithValue("@GetsPaid", null);
                command.Parameters.AddWithValue("@GetsResourcesPaid", null);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddMakeUpArtist(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Contact (FirstName, LastName, Picture, Location, Description, ExtraInformation, Naked, GetsPaid, GetsResourcesPaid) VALUES (@FirstName, @LastName, @Picture, @Location, @Description, @ExtraInformation, @Naked, GetsPaid, GetsResourcesPaid)", connection))
            {
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description", contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", null);
                command.Parameters.AddWithValue("@GetsPayed", null);
                command.Parameters.AddWithValue("@GetsResourcesPayed", contact.GetsResourcesPaid);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddHelper(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Contact (FirstName, LastName, Picture, Location, Description, ExtraInformation, Naked, GetsPaid, GetsResourcesPaid) VALUES (@FirstName, @LastName, @Picture, @Location, @Description, @ExtraInformation, @Naked, @GetsPaid, @GetsResourcesPaid)", connection))
            {
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description", contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", null);
                command.Parameters.AddWithValue("@GetsPaid", contact.GetsPaid);
                command.Parameters.AddWithValue("@GetsResourcesPaid", null);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Contact SET FirstName = @FirstName, LastName = @LastName, Picture = @Picture, Location = @Location, Description = @Description, ExtraInformation = @ExtraInformation, Naked = @Naked, GetsPaid = @GetsPaid, GetsResourcesPaid = @GetsResourcesPaid, WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", contact.Id);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description", contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", contact.Naked);
                command.Parameters.AddWithValue("@GetsPaid", contact.GetsPaid);
                command.Parameters.AddWithValue("@GetsResourcesPaid", contact.GetsResourcesPaid);

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

        public List<Location> GetAllLocations()
        {
            var locations = new List<Location>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Location", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        locations.Add(new Location(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                    }
                }
            }
            return locations;
        }

        public void AddLocation(Location location)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Location (Street, HouseNumber, PostalCode, City, Country) VALUES (@Street, @HouseNumber, @PostalCode, @City, @Country)", connection))
            {
                command.Parameters.AddWithValue("@Street", location.Street);
                command.Parameters.AddWithValue("@HouseNumber", location.HouseNumber);
                command.Parameters.AddWithValue("@PostalCode", location.PostalCode);
                command.Parameters.AddWithValue("@City", location.City);
                command.Parameters.AddWithValue("@Country", location.Country);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateLocation(Location location)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Location SET Street = @Street, HouseNumber = @HouseNumber, PostalCode = @PostalCode, City = @City, Country = @Country WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", location.Id);
                command.Parameters.AddWithValue("@Street", location.Street);
                command.Parameters.AddWithValue("@HouseNumber", location.HouseNumber);
                command.Parameters.AddWithValue("@PostalCode", location.PostalCode);
                command.Parameters.AddWithValue("@City", location.City);
                command.Parameters.AddWithValue("@Country", location.Country);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteLocation(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM Location WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Location GetLocationByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Location WHERE City = @City", connection))
            {
                command.Parameters.AddWithValue("@City", name);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Location(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    }
                }
            }
            throw new Exception(nameof(GetLocationByName));
        }
    }
}
