using Casus4;

public class Model : Contact
{
    DAL dal = new DAL();
    public bool GetsPayed { get; set; }

    public Model(int? id, string firstName, string lastName, byte[] picture, Location location, string description, string extraInformation, bool naked)
            : base(id, firstName, lastName, picture, 0, location, description, extraInformation, naked)
    {
    }
    public void Add(Model model)
    {
        throw new NotImplementedException();
    }
    public override void Remove(int id)
    {
        throw new NotImplementedException();
    }
    public void Edit(Model model, int id)
    {
        throw new NotImplementedException();
    }
    public override void FilterOn(string Filter)
    {
        throw new NotImplementedException();
    }
    public override void SearchOn(string searchCriteria)
    {
        throw new NotImplementedException();
    }
    public override Model SearchOnName(string searchCriteria)
    {
        return dal.FindModelByName(searchCriteria);
    }
}
