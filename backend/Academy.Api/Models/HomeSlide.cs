namespace Academy.Api.Models;

public class HomeSlide : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ButtonText { get; set; } = string.Empty;
    public string ButtonLink { get; set; } = string.Empty;
}
