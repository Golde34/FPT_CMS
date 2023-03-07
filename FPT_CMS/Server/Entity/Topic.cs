namespace Server.Entity
{
    public class Topic
    {
        public Topic()
        {
            Submissions = new List<Submission>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Requirement { get; set; }
        public DateTime  Due { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
