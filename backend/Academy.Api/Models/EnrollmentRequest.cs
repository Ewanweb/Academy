namespace Academy.Api.Models;

public class EnrollmentRequest : BaseEntity
{
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    public string Status { get; set; } = "Pending";
    public string Note { get; set; } = string.Empty;
}
