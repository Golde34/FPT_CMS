using System.ComponentModel.DataAnnotations;

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
        public DateTime Due { get; set; }
        public string? CourseId { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
        public virtual Course? Course { get; set; }
    }
}
