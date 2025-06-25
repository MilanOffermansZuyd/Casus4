using Casus4;

public abstract class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] Picture { get; set; }
    public int DistanceBetween { get; set; }
    public string Location { get; set; }
    public string Rol { get; set; }
    public string Description { get; set; }
    public string ExtraInformation { get; set; }
    public bool Naked { get; set; }
    public Contact(int? id, string firstName, string lastName, byte[] picture, int distanceBetween, string location, string rol, string description, string extrainformation, bool naked)
    {
        Id = id ?? 0;
        FirstName = firstName;
        LastName = lastName;
        Picture = picture;
        DistanceBetween = distanceBetween;
        Location = location;
        Rol = rol;
        Description = description;
        ExtraInformation = extrainformation;
        Naked = naked;
    }


    DAL dal = new DAL();

    public virtual void Add(Contact contact)
    {
        dal.AddContact(contact);
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
}
