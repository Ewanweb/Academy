namespace Academy.Api.Models;

public class CourseCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Course>? Courses { get; set; }
}
