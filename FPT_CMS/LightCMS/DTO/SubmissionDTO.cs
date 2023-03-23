namespace LightCMS.DTO
{
    public class SubmissionDTO
    {
        public int Id { get; set; }
        public DateTime SubmitDate { get; set; }
        public string URL { get; set; }
        public int? StudentId { get; set; }
        public int? TopicId { get; set; }
    }
}
