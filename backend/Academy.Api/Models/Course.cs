namespace Academy.Api.Models;

public class Course : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Level { get; set; } = "مقدماتی";
    public string Mode { get; set; } = "حضوری";
    public decimal Price { get; set; }
    public int DurationWeeks { get; set; }
    public string Prerequisites { get; set; } = string.Empty;
    public string Syllabus { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public CourseCategory? Category { get; set; }
    public Guid InstructorId { get; set; }
    public Instructor? Instructor { get; set; }
    public ICollection<EnrollmentRequest>? EnrollmentRequests { get; set; }
}
