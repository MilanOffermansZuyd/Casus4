using Casus4;

public abstract class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] Picture { get; set; }
    public Location Location { get; set; }

    public Contact(int? id,string firstName, string lastName, byte[] picture, Location location)
    {
        Id = id ?? 0;
        FirstName = firstName;
        LastName = lastName;
        Picture = picture;
        Location = location;
    }


    public virtual void Add(Contact contact)
    {
        throw new NotImplementedException();
    }
    public virtual void Remove(int id)
    {
        throw new NotImplementedException();
    }
    public virtual void Edit(Contact contact, int id)
    {
        throw new NotImplementedException();
    }
    public virtual void FilterOn(string Filter)
    {
        throw new NotImplementedException();
    }
    public virtual void SearchOn(string searchCriteria)
    {
        throw new NotImplementedException();
    }
}
