using Casus4;
using static Azure.Core.HttpHeader;

public class Model : Contact
{
    DAL dal = new DAL();
    public bool GetsPayed { get; set; }

    public Model(int? id, string firstName, string lastName, byte[] picture, int distanceBetween, Location location, string rol, string description, string extrainformation, bool naked, bool getsPayed)
        : base(id, firstName, lastName, picture, distanceBetween, location, rol, description, extrainformation, naked)
    {
        GetsPayed = getsPayed;
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
    //public override Model SearchOnName(string searchCriteria)
    //{
    //    return dal.FindModelByName(searchCriteria);
    //}
}
