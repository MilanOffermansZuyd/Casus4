namespace Casus4
{
    public class Concept
    {
        public string Location { get; set; }
        public List<Model>? Models{ get; set; }
        public List<Helper>? Extras{ get; set; }
        public List<string>? Props { get; set; }
        public string Title { get; set; }
        public List<PhotoShoot>? Photoshoots{ get; set; }

        public Concept(string location, List<Model>? models, List<Helper>? extras, List<string>? props, string title, List<PhotoShoot>? photoShoots) 
        {
            Location = location;
            Models = models ?? null;
            Extras = extras ?? null;
            Props = props ?? null;
            Title = title;
            Photoshoots = photoShoots ?? null;

        }

        public void Add(Concept concept) 
        {
            throw new NotImplementedException();    
        }
        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
        public void Edit(Concept concept, int id)
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
}
