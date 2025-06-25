using Casus4;

public class PhotoShoot 
{
    DAL dal = new DAL();
    public int Id { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public List<Concept> Concepts { get; set; }
    public Contract Contract { get; set; }

    public PhotoShoot(int id,string title, string subTitle, List<Concept> concepts, Contract contract)
    {
        Id = id;
        Title = title;
        SubTitle = subTitle;
        Concepts = concepts;
        Contract = contract;
    }
    public void Add(PhotoShoot photoshoot)
    {
        dal.AddPhotoshoot(photoshoot);
    }
    public void AddConceptPhotoshoot(Concept concept)
    {
        dal.AddConceptPhotoshoot(concept);
    }
    public void AddPhotoshootModel(Model model)
    {
        dal.AddPhotoshootModel(model);
    }

    public void Remove(int id)
    {
        dal.DeletePhotoshoot(id);
    }
    public void Edit(PhotoShoot photoshoot, int id)
    {
        throw new NotImplementedException();
    }
    public void FilterOn(string Filter)
    {
        throw new NotImplementedException();
    }
    public void SearchOn(string searchCriteria)
    {
        throw new NotImplementedException();
    }

}
