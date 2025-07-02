namespace Casus4
{
    public class Adress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public Adress(int? id, string street, string houseNumber, string postalCode, string city)
        {
            Id = id??0;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
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
