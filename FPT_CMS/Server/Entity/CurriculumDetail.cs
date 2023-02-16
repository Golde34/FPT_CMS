namespace Server.Entity
{
    public class CurriculumDetail
    {
        public int CurriculumId { get; set; }
        public Curriculum Curriculum { get; set; }
        public string SubjectCode { get; set; }
        public Subject Subject { get; set; }
    }
}
