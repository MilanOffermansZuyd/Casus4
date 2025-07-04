namespace Casus4
{
    public class Adress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
        public Adress(int? id, string street, string houseNumber, string postalCode, string city, Country country)
        {
            Id = id??0;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }
        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
