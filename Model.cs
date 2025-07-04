using Casus4;
using static Azure.Core.HttpHeader;

public class Model : PayedContact
{
    DAL dal = new DAL();
    public bool Naked { get; set; }

    public Model(int? id, string firstName, string lastName, byte[] picture, Location location, string description, string extraInformation,bool payed ,bool naked)
            : base(id, firstName, lastName, picture, location, description, extraInformation, payed)
    {
        Naked = naked;
    }
    public void Add(Model model)
    {
        dal.AddModel(model);
    }
    public override void Remove(int id)
    {
        dal.DeleteContact(id);
    }
    public void Edit(Model model, int id)
    {
        throw new NotImplementedException();
    }
}
