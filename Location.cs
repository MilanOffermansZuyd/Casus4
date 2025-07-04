namespace Casus4
{
    public class Location
    {
        DAL dal = new DAL();
        public int Id { get; set; }
        public string Name { get; set; }
        public LocalAuthority? LocalAuthority { get; set; }
        public Adress? Adress { get; set; }
        public Country? Country { get; set; }

        public Location(int? id, string name, LocalAuthority? localAuthority, Adress? adress, Country? country) 
        {
            Id = id ?? 0;
            Name = name;
            LocalAuthority = localAuthority;
            Adress = adress;
            Country = country;
        }

        public Location()
        {
            
        }


        public List<Location> GetAll()
        {
            throw new NotImplementedException();
        }

        public void add(Location location) 
        {
            dal.AddLocation(location);
        }

        public Location Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
