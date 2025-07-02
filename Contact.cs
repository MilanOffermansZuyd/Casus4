using Casus4;

public abstract class Contact
{
    DAL dal = new DAL();
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] Picture { get; set; }
    public int DistanceBetween { get; set; }
    public Location Location { get; set; }
    public string Description { get; set; }
    public string ExtraInformation { get; set; }
    public Contact(int? id, string firstName, string lastName, byte[] picture, int distanceBetween, Location location, string description, string extrainformation)
    {
        Id = id ?? 0;
        FirstName = firstName;
        LastName = lastName;
        Picture = picture;
        DistanceBetween = distanceBetween;
        Location = location;
        Description = description;
        ExtraInformation = extrainformation;
    }

    public virtual void Add(Model contact)
    {
        dal.AddModel(contact);
    }
    public virtual void Remove(int id)
    {
        dal.DeleteContact(id);
    }
    public virtual void Edit(Contact contact)
    {
        dal.UpdateContact(contact);
    }
    public virtual void FilterOn(string Filter)
    {
        throw new NotImplementedException();
    }
    public virtual void SearchOn(string searchCriteria)
    {
        throw new NotImplementedException();
    }
    public virtual object SearchOnName(string searchCriteria)
    {
        throw new NotImplementedException();
    }
}
