public abstract class Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] Picture { get; set; }
    public int DistanceBetween { get; set; }

    public Contact(string firstName, string lastName, byte[] picture, int distanceBetween)
    {
        FirstName = firstName;
        LastName = lastName;
        Picture = picture;
        DistanceBetween = distanceBetween;
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
