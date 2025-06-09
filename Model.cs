public class Model : Contact
{
    public bool GetsPayed { get; set; }
    public bool IsModel { get; set; }

    public Model(string firstName, string lastName, byte[] picture, int distanceBetween, bool getsPayed, bool isModel)
        : base(firstName, lastName, picture, distanceBetween)
    {
        GetsPayed = getsPayed;
        IsModel = isModel;
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
}
