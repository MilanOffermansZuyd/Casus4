using Casus4;

public class PhotoShoot 
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public List<Concept> Concepts { get; set; }
    public byte[] FotoSketch { get; set; }
    public List<byte[]> FotoResult { get; set; }
    public List<Contract> Contracts { get; set; }

    public PhotoShoot(int id, string title, string subTitle, List<Concept> concepts, byte[] fotosketch, List<byte[]> fotoResult, List<Contract> contracts)
    {
        Id = id;
        Title = title;
        SubTitle = subTitle;
        Concepts = concepts;
        FotoSketch = fotosketch;
        FotoResult = fotoResult;
        Contracts = contracts;

    }
    public void Add(PhotoShoot fotoShoot)
    {
        throw new NotImplementedException();
    }
    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
    public void Edit(PhotoShoot fotoShoot, int id)
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
