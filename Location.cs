namespace Casus4
{
    public class Location
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Location(int? id, string street, string houseNumber, string postalCode, string city, string country) 
        {
            Id = id ?? 0;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }


        public List<Location> GetAll()
        {
            return [new Location(0, "t", "", "", "", "")];
        }

        public Location add(Location location) 
        {
            return new Location(0, "t", "", "", "", "");
        }

        public Location Remove(int id)
        {
            return new Location(0, "t", "", "", "", "");
        }
    }
}
