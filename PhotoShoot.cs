using Casus4;

public class PhotoShoot 
{
    DAL dal = new DAL();
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Location Location { get; set; }
    public List<Concept> Concepts { get; set; }
    public List<Contract> Contracts { get; set; }
    public List<Model> Models { get; set; }
    public List<Prop> Props { get; set; }

    public PhotoShoot(int id, DateTime date, Location location, List<Concept> concepts, List<Contract> contracts, List<Model> models, List<Prop> props)
    {
        Id = id;
        Date = date;
        Location = location;
        Concepts = concepts;
        Contracts = contracts;
        Models = models;
        Props = props;
    }
    public void Add()
    {
        PhotoShoot photoshoot = this;
        dal.AddPhotoshoot(photoshoot);
    }

    public void Remove(int id)
    {
        dal.DeletePhotoshoot(id);
    }
    public void Edit()
    {
        PhotoShoot photoshoot = this;
        dal.UpdatePhotoshoot(photoshoot);
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
