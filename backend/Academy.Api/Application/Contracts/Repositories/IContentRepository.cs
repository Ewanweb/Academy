using Academy.Api.Application.DTOs;
using Academy.Api.Models;

namespace Academy.Api.Application.Contracts.Repositories;

public interface IContentRepository
{
    Task<HomeDataDto> GetHomeDataAsync();
    Task<(IEnumerable<BlogPost> Items, int Total)> GetBlogPostsAsync(int page, int pageSize);
    Task<BlogPost?> GetBlogPostBySlugAsync(string slug);

    Task<IEnumerable<CourseCategory>> GetCategoriesAsync();
    Task<CourseCategory> AddCategoryAsync(CourseCategory category);
    Task<CourseCategory?> FindCategoryAsync(Guid id);
    Task UpdateCategoryAsync(CourseCategory category);
    Task DeleteCategoryAsync(CourseCategory category);

    Task<IEnumerable<Instructor>> GetInstructorsAsync();
    Task<Instructor> AddInstructorAsync(Instructor instructor);
    Task<Instructor?> FindInstructorAsync(Guid id);
    Task UpdateInstructorAsync(Instructor instructor);
    Task DeleteInstructorAsync(Instructor instructor);

    Task<BlogPost> AddBlogPostAsync(BlogPost post);
    Task<BlogPost?> FindBlogPostAsync(Guid id);
    Task UpdateBlogPostAsync(BlogPost post);
    Task DeleteBlogPostAsync(BlogPost post);

    Task<IEnumerable<HomeSlide>> GetSlidesAsync();
    Task<HomeSlide> AddSlideAsync(HomeSlide slide);
    Task<HomeSlide?> FindSlideAsync(Guid id);
    Task UpdateSlideAsync(HomeSlide slide);
    Task DeleteSlideAsync(HomeSlide slide);

    Task<IEnumerable<Testimonial>> GetTestimonialsAsync();
    Task<Testimonial> AddTestimonialAsync(Testimonial testimonial);
    Task<Testimonial?> FindTestimonialAsync(Guid id);
    Task UpdateTestimonialAsync(Testimonial testimonial);
    Task DeleteTestimonialAsync(Testimonial testimonial);

    Task<IEnumerable<FAQ>> GetFaqsAsync();
    Task<FAQ> AddFaqAsync(FAQ faq);
    Task<FAQ?> FindFaqAsync(Guid id);
    Task UpdateFaqAsync(FAQ faq);
    Task DeleteFaqAsync(FAQ faq);

    Task<IEnumerable<StoryHighlight>> GetStoriesAsync();
    Task<StoryHighlight> AddStoryAsync(StoryHighlight story);
    Task<StoryHighlight?> FindStoryAsync(Guid id);
    Task UpdateStoryAsync(StoryHighlight story);
    Task DeleteStoryAsync(StoryHighlight story);
}
