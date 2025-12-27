using Academy.Api.Models;

namespace Academy.Api.Application.DTOs;

public class HomeDataDto
{
    public IEnumerable<HomeSlide> Slides { get; set; } = Enumerable.Empty<HomeSlide>();
    public IEnumerable<Testimonial> Testimonials { get; set; } = Enumerable.Empty<Testimonial>();
    public IEnumerable<FAQ> Faqs { get; set; } = Enumerable.Empty<FAQ>();
    public IEnumerable<CourseCategory> Categories { get; set; } = Enumerable.Empty<CourseCategory>();
    public IEnumerable<Course> TopCourses { get; set; } = Enumerable.Empty<Course>();
    public IEnumerable<BlogPost> LatestPosts { get; set; } = Enumerable.Empty<BlogPost>();
    public IEnumerable<StoryHighlight> Stories { get; set; } = Enumerable.Empty<StoryHighlight>();
}
