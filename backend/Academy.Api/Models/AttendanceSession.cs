namespace Academy.Api.Models;

public class AttendanceSession : BaseEntity
{
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public DateTime SessionDate { get; set; } = DateTime.UtcNow;
    public string Title { get; set; } = string.Empty;
    public ICollection<AttendanceRecord>? Records { get; set; }
}
