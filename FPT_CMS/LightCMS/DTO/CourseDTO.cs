namespace LightCMS.DTO;

public class CourseDTO
{
    public string CourseId { get; set; }
    public string CourseName { get; set; }
    public int Slot { get; set; }
    public string SemesterId { get; set; }
    public string? SubjectCode { get; set; }
    public string? TeacherId { get; set; }
}