namespace Casus4
{
    public class Project
    {
        private DAL dal = new DAL();
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime Deadline { get; set; }
        public List<Concept>? Concepts { get; set; }

        public Project (int? id, string title, string subTitle, DateTime deadline, List<Concept>? concepts) 
        {
            Id = id ?? 0;
            Title = title;
            SubTitle = subTitle;    
            Deadline = deadline;
            Concepts = concepts ?? null;

        }

        public Project() 
        {
        
        }

        public List<Project> Get() 
        {
            return dal.GetAllProjects();
        }

        public void Add() 
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            throw new NotImplementedException();
        }
        public void FilterOn()
        {
            throw new NotImplementedException();
        }
        public void SearchOn()
        {
            throw new NotImplementedException();
        }
    }
}
