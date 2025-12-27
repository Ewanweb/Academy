namespace Academy.Api.Models;

public class Instructor : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string Expertise { get; set; } = string.Empty;
    public ICollection<Course>? Courses { get; set; }
}
