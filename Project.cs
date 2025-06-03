namespace Casus4
{
    public class Project
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime Deadline { get; set; }
        public List<Concept>? Concepts { get; set; }

        public Project (string title, string subTitle, DateTime deadline, List<Concept>? concepts) 
        {
            Title = title;
            SubTitle = subTitle;    
            Deadline = deadline;
            Concepts = concepts ?? null;

        }

    }
}
