namespace Casus4
{
    public class Concept
    {
        private DAL dal = new DAL();
        public int Id { get; set; }
        public string Title { get; set; }
        public Location Location { get; set; }
        public byte[] FotoSketch { get; set; }
        public List<byte[]> FotoResult { get; set; }
        public List<Model>? Models{ get; set; }
        public List<Helper>? Extras{ get; set; }
        public Project Project { get; set; }

        public Concept(int? id, string title, Location location, byte[] fotoSketch , List<byte[]> fotoResult, Project project, List<Model>? models, List<Helper>? extras) 
        {
            Id = id ?? 0;
            Location = location;
            FotoResult = fotoResult;
            FotoSketch = fotoSketch;
            Models = models;
            Extras = extras;
            Project = project;
            Title = title;

        }
        public Concept()
        {
            
        }
        public List<Concept> get()
        {
            return dal.GetAllConcepts();
        }

        public Concept Add(Concept concept) 
        {
            return dal.AddConcept(concept);
        }
        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
        public void Edit(Concept concept)
        {
            dal.UpdateConcept(concept);
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
