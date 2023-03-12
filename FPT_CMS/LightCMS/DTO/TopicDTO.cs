namespace LightCMS.DTO
{
    public class TopicDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Requirement { get; set; }
        public DateTime Due { get; set; }
        public string? CourseId { get; set; }
    }
}
