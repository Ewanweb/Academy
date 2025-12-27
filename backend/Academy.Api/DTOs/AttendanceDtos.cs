namespace Academy.Api.DTOs;

public class AttendanceSessionCreateDto
{
    public Guid CourseId { get; set; }
    public DateTime SessionDate { get; set; } = DateTime.UtcNow;
    public string Title { get; set; } = string.Empty;
}

public class AttendanceRecordCreateDto
{
    public Guid SessionId { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = "Present";
    public string Note { get; set; } = string.Empty;
}
