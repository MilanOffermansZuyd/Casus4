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
        public string? Description { get; set; }

        public Concept(int? id, string title, Location location, byte[] fotoSketch , List<byte[]> fotoResult, Project project, List<Model>? models, List<Helper>? extras, string? desciption) 
        {
            Id = id ?? 0;
            Location = location;
            FotoResult = fotoResult;
            FotoSketch = fotoSketch;
            Models = models;
            Extras = extras;
            Project = project;
            Title = title;
            Description = desciption;
        }
        public Concept()
        {
            
        }
        public List<Concept> get()
        {
            return dal.GetAllConcepts();
        }

        public void Add(Concept concept) 
        {
            dal.AddConcept(concept);
        }
        public void Remove(int Id)
        {
            dal.DeleteConcept(Id);
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
