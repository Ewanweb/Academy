namespace Academy.Api.Models;

public class AttendanceRecord : BaseEntity
{
    public Guid AttendanceSessionId { get; set; }
    public AttendanceSession? Session { get; set; }
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    public string Status { get; set; } = "Present"; // Present / Absent / Late
    public string Note { get; set; } = string.Empty;
}
