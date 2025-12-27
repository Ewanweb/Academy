namespace Academy.Api.Models;

public class Testimonial : BaseEntity
{
    public string StudentName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int Rating { get; set; } = 5;
}
