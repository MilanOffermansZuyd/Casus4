namespace Casus4
{
    public class Location
    {
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


        public Location add(Location location) 
        {
            throw new NotImplementedException();
        }

        public Location Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
