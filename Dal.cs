using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace Casus4
{
    public class DAL
    {

        private readonly string connectionString = "Data Source=LAPTOP-T4RLVBV6;Initial Catalog=IdeaToGoCasus4;Integrated Security=True;Trust Server Certificate=True";
        
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

                        projects.Add(new Project(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),  reader.GetDateTime(3), null));
                    }
                }
            }

            return projects;
        }


        public Project FindProject(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Project WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id",id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Project(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), null);
                    }
                }
            }
            throw new Exception(nameof(FindProject));
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

                        PhotoShoot photoshoot = new PhotoShoot(0, new DateTime(2000, 1, 1), null, null, null, null, null);
                        photoshoot.Id = reader.GetInt32(0);
                        photoshoot.Location = GetLocationById(reader.GetInt32(1));
                        photoshoot.Date = reader["Date"] as DateTime? ?? null;
                        photoshoot.Concepts = GetPhotoshootConcepts(reader.GetInt32(0));
                        photoshoot.Contracts = GetPhotoshootContracts(reader.GetInt32(0));
                        photoshoot.Models = GetPhotoshootModels(reader.GetInt32(0));
                        photoshoot.Props = GetPhotoshootProps(reader.GetInt32(0));
                    }
                }
            }

            return photoshoots;
        }

        public PhotoShoot GetPhotoshootById(int Id)
        {

            PhotoShoot photoshoot = new PhotoShoot(0, new DateTime(2000, 1, 1), null, null, null, null, null);
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Photoshoot WHERE Id = @id", connection))
            {
                command.Parameters.AddWithValue("@Id", Id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        photoshoot.Id = reader.GetInt32(0);
                        photoshoot.Location = GetLocationById(reader.GetInt32(1));
                        photoshoot.Date = reader.GetDateTime(2);
                        photoshoot.Concepts = GetPhotoshootConcepts(reader.GetInt32(0));
                        photoshoot.Contracts = GetPhotoshootContracts(reader.GetInt32(0));
                        photoshoot.Models = GetPhotoshootModels(reader.GetInt32(0));
                        photoshoot.Props = GetPhotoshootProps(reader.GetInt32(0));
                    }
                }
                return photoshoot;
            }
        }

        public List<Concept> GetPhotoshootConcepts(int PhotoshootId)
        {
            List<Concept> concepts = new List<Concept>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT C.ID, C.Title FROM CONCEPT C, ConceptPhotoshoots P WHERE C.Id = P.ConceptId AND P.PhotoshootId = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", PhotoshootId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Concept concept = new Concept();
                        concept.Id = reader.GetInt32(0);
                        concept.Title = reader.GetString(1);
                        concepts.Add(concept);
                    }
                }
                return concepts;
            }
        }
        public List<Contract> GetPhotoshootContracts(int PhotoshootId)
        {
            List<Contract> contracts = new List<Contract>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT ID, Name FROM Contract WHERE ForPhotoshoot = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", PhotoshootId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contract contract = new Contract(0, null,null,false,null);
                        contract.Id = reader.GetInt32(0);
                        contract.Name = reader.GetString(1);
                        contracts.Add(contract);
                    }
                }
                return contracts;
            }
        }
        public List<Model> GetPhotoshootModels(int PhotoshootId)
        {
            List<Model> models = new List<Model>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT C.Id, C.FirstName, C.LastName FROM CONTACT C, PhotoshootContact P WHERE C.Id = P.ContactId AND P.PhotoshootId = @Id AND C.Naked IS NOT NULL", connection))
            {
                command.Parameters.AddWithValue("@Id", PhotoshootId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Model model = new Model(0, null,null,null,null,null,null,false);
                        model.Id = reader.GetInt32(0);
                        model.FirstName = reader.GetString(1);
                        model.LastName = reader.GetString(2);
                        model.Add(model);
                    }
                }
                return models;
            }
        }
        public List<Prop> GetPhotoshootProps(int PhotoshootId)
        {
            List<Prop> props = new List<Prop>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT P.ID, P.Name FROM Prop P, PhotoShootProps A WHERE P.Id = A.PropId AND A.PhotoshootId = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", PhotoshootId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Prop prop = new Prop(0, null, null);
                        prop.Id = reader.GetInt32(0);
                        prop.Name = reader.GetString(1);
                        props.Add(prop);
                    }
                }
                return props;
            }
        }

        public void AddPhotoshoot(PhotoShoot photoShoot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Photoshoot (LocationId, Date) VALUES (@Location, @Date)", connection))
            {
                command.Parameters.AddWithValue("@Location", photoShoot.Location.Id);
                command.Parameters.AddWithValue("@Date", photoShoot.Date);

                connection.Open();
                command.ExecuteNonQuery();
            }

            foreach (Concept concept in photoShoot.Concepts)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("INSERT INTO ConceptPhotoshoots (ConceptId, PhotoshootId,) VALUES (@ConceptId, @PhotoshootId)", connection))
                {
                    command.Parameters.AddWithValue("@ConceptId", concept.Id);
                    command.Parameters.AddWithValue("@PhotoshootId", photoShoot.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            foreach (Contract contract in photoShoot.Contracts)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("UPDATE Contract SET ForPhotoshoot = @PhotoshootId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", contract.Id);
                    command.Parameters.AddWithValue("@PhotoshootId", photoShoot.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            foreach (Model model in photoShoot.Models)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("INSERT INTO PhotoshootContact (PhotoshootId, ContactId) VALUES (@PhotoshootId, @ContactId)", connection))
                {
                    command.Parameters.AddWithValue("@ContactId", model.Id);
                    command.Parameters.AddWithValue("@PhotoshootId", photoShoot.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            foreach (Prop prop in photoShoot.Props)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("INSERT INTO PhotoshootProps (PhotoshootId, PropId) VALUES (@PhotoshootId, @PropId)", connection))
                {
                    command.Parameters.AddWithValue("@PropId", prop.Id);
                    command.Parameters.AddWithValue("@PhotoshootId", photoShoot.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePhotoshoot(PhotoShoot photoshoot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Photoshoot SET Title = @Title, Subtitle = @Subtitle, Contract = @Contract, LocationId = @Location WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", photoshoot.Id);
                command.Parameters.AddWithValue("@Location", photoshoot.Location.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }

            //Delete all concepts with photoshoot ID from table, then add in the selected
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM ConceptPhotoshoots WHERE PhotoshootId = @PhotoshootId", connection))
            {
                command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }

            foreach (Concept concept in photoshoot.Concepts)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("INSERT INTO ConceptPhotoshoots (ConceptId, PhotoshootId,) VALUES (@ConceptId, @PhotoshootId)", connection))
                {
                    command.Parameters.AddWithValue("@ConceptId", concept.Id);
                    command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            //Update contracts with photoshoot, then re-add the photoshoot ID
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Contract SET ForPhotoshoot = null WHERE PhotoshootId = @PhotoshootId", connection))
            {
                command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }

            foreach (Contract contract in photoshoot.Contracts)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("UPDATE Contract SET ForPhotoshoot = @PhotoshootId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", contract.Id);
                    command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }


            // Remove all contacts with photoshoot ID, then re-add them
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM PhotoshootContact WHERE PhotoshootId = @PhotoshootId", connection))
            {
                command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }

            foreach (Model model in photoshoot.Models)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("INSERT INTO PhotoshootContact (PhotoshootId, ContactId) VALUES (@PhotoshootId, @ContactId)", connection))
                {
                    command.Parameters.AddWithValue("@ContactId", model.Id);
                    command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Remove all props with photoshoot ID, then re-add them
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM PhotoshootProps WHERE PhotoshootId = @PhotoshootId", connection))
            {
                command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            foreach (Prop prop in photoshoot.Props)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("INSERT INTO PhotoshootProps (PhotoshootId, PropId) VALUES (@PhotoshootId, @PropId)", connection))
                {
                    command.Parameters.AddWithValue("@PropId", prop.Id);
                    command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);
                        
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }

        public void DeletePhotoshoot(PhotoShoot photoshoot)
        {
            //Concepts
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM ConceptPhotoshoots WHERE PhotoshootId = @PhotoshootId", connection))
            {
                command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            //contracts
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Contract SET ForPhotoshoot = null WHERE PhotoshootId = @PhotoshootId", connection))
            {
                command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            //contacts
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM PhotoshootContact WHERE PhotoshootId = @PhotoshootId", connection))
            {
                command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            //Props
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM PhotoshootProps WHERE PhotoshootId = @PhotoshootId", connection))
            {
                command.Parameters.AddWithValue("@PhotoshootId", photoshoot.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }

            //Photoshoot
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM Photoshoot WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", photoshoot.Id);
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

                        concepts.Add(new Concept(reader.GetInt32(0), reader.GetString(1), reader["LocationId"] as Location ?? null, reader["PhotoSketch"] as byte[] ?? null, ConceptImageHelper.SplitPhotos(reader["PhotoResults"] as byte[] ?? null), FindProject((int)reader["ProjectId"]), null, null, reader["Description"] as string ?? null)); ;
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

            using (SqlCommand command = new SqlCommand("INSERT INTO Concept (Title, LocationId,PhotoSketch, ProjectId, Description) VALUES (@Title, @LocationId,@PhotoSketch,@ProjectId ,@Description )", connection))
            {
                command.Parameters.AddWithValue("@Title", concept.Title);
                command.Parameters.AddWithValue("@LocationId", concept.Location?.Id ?? 1);

                command.Parameters.Add("@PhotoSketch", SqlDbType.VarBinary).Value =
                    (object?)concept.FotoSketch ?? DBNull.Value;

                command.Parameters.AddWithValue("@ProjectId", concept.Project.Id);
                command.Parameters.AddWithValue("@Description", concept.Description);


                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateConcept(Concept concept)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Concept SET LocationId = @LocationId, Title = @Title , ProjectId = @ProjectId ,Description =@Description,PhotoSketch = @PhotoSketch, PhotoResults =@PhotoResults WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", concept.Id);
                command.Parameters.AddWithValue("@Title", concept.Title);
                command.Parameters.AddWithValue("@LocationId", concept.Location?.Id ?? 1);

                command.Parameters.Add("@PhotoSketch", SqlDbType.VarBinary).Value =
                    (object?)concept.FotoSketch ?? DBNull.Value;

                var bytefotos = ConceptImageHelper.CombinePhotos(concept.FotoResult);
                command.Parameters.Add("@PhotoResults", SqlDbType.VarBinary).Value =
                    (object?)bytefotos ?? DBNull.Value;

                command.Parameters.AddWithValue("@ProjectId", concept.Project.Id);
                command.Parameters.AddWithValue("@Description", concept.Description);



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
                        contacts.Add(new Model(id, firstName, lastName, picture, location, description, extraInformation, naked));

                    }
                }
            }
            return contacts;
        }

        public List<Contact> GetAllMakeUpArtists()
        {
            var contacts = new List<Contact>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contact WHERE GetsResourcesPayed IS NOT NULL", connection))
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
                        Location location = dal.GetLocationById(locationId);
                        string description = reader["Description"].ToString();
                        string extraInformation = reader["ExtraInfo"].ToString();
                        bool getsResourcesPayed = (bool)reader["GetsResourcesPayed"];
                        contacts.Add(new MakeUpArtist(id, firstName, lastName, picture, location, description, extraInformation, null, null, getsResourcesPayed));
                    }
                }
            }
            return contacts;
        }

        public List<Contact> GetAllHelpers()
        {
            var contacts = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Contact WHERE GetsPayed IS NOT NULL", connection))
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
                        Location location = dal.GetLocationById(locationId);
                        string description = reader["Description"].ToString();
                        string extraInformation = reader["ExtraInfo"].ToString();
                        bool getsPayed = (bool)reader["GetsPayed"];
                        contacts.Add(new Helper(id, firstName, lastName, picture, location, description, extraInformation, null, getsPayed, null));
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

                        Model model = new(Id, FirstName, LastName, Picture, location, description, extraInformation, false);

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
            using (SqlCommand command = new SqlCommand("INSERT INTO Contact (FirstName, LastName, Picture, Location, Description, ExtraInformation, Naked) VALUES (@FirstName, @LastName, @Picture, @Location, @Description, @ExtraInformation, @Naked)", connection))
            {
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description", contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", contact.Naked);
                command.Parameters.AddWithValue("@GetsPayed", null);
                command.Parameters.AddWithValue("@GetsResourcesPayed", null);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddMakeUpArtist(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Contact (FirstName, LastName, Picture, Location, Description, ExtraInformation, Naked) VALUES (@FirstName, @LastName, @Picture, @Location, @Description, @ExtraInformation, @Naked)", connection))
            {
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description", contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", null);
                command.Parameters.AddWithValue("@GetsPayed", null);
                command.Parameters.AddWithValue("@GetsResourcesPayed", contact.GetsResourcesPayed);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddHelper(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Contact (FirstName, LastName, Picture, Location, Description, ExtraInformation, Naked) VALUES (@FirstName, @LastName, @Picture, @Location, @Description, @ExtraInformation, @Naked)", connection))
            {
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description", contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", null);
                command.Parameters.AddWithValue("@GetsPayed", contact.GetsPayed);
                command.Parameters.AddWithValue("@GetsResourcesPayed", null);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UPDATE Contact SET FirstName = @FirstName, LastName = @LastName, Picture = @Picture, Location = @Location, Description = @Description, ExtraInformation = @ExtraInformation, Naked = @Naked, WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", contact.Id);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Picture", contact.Picture);
                command.Parameters.AddWithValue("@Location", contact.Location);
                command.Parameters.AddWithValue("@Description", contact.Description);
                command.Parameters.AddWithValue("@ExtraInformation", contact.ExtraInformation);
                command.Parameters.AddWithValue("@Naked", contact.Naked);
                command.Parameters.AddWithValue("@GetsPayed", contact.GetsPayed);
                command.Parameters.AddWithValue("@GetsResourcesPayed", contact.GetsResourcesPayed);

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
            Location location = new Location();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Location WHERE Id = @Id", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", Id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        location.Name = reader.GetString(1);
                        location.Adress= reader["HouseNumber"] as Adress ?? null;
                        location.LocalAuthority = reader["PostalCode"] as LocalAuthority ?? null;
                        location.Country = reader["Country"] as Country ?? null;
                    }
                    return location;
                }
            }
            throw new Exception(nameof(GetLocationById));
        }

        public Location GetLocationByString(string street)
        {
            Location location = new Location();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Location WHERE Street = @Street", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Street", street);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        location.Name = reader.GetString(1);
                        location.Adress = reader["HouseNumber"] as Adress ?? null;
                        location.LocalAuthority = reader["PostalCode"] as LocalAuthority ?? null;
                        location.Country = reader["Country"] as Country ?? null;
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
              
        //CRUD for Props
        internal List<Prop> GetAllProps()
        {

            var props = new List<Prop>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Prop", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string Name = reader.GetString(1);
                        string Description = reader.GetString(2);
                        props.Add(new Prop(id, Name, Description));
                    }
                }
            }
            return props;

        }
    }
}
