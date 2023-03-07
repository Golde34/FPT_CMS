namespace Server.Entity
{
    public class Submission
    {
        public int Id { get; set; }
        public DateTime SubmitDate { get; set; }
        public string URL { get; set; }
        public int? StudentId { get; set; }
        public int? TopicId { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Topic? Topic { get; set; }
    }
}
