using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Application.DTOs;
using Academy.Api.Data;
using Academy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Api.Infrastructure.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly AcademyDbContext _db;

    public ContentRepository(AcademyDbContext db)
    {
        _db = db;
    }

    public async Task<HomeDataDto> GetHomeDataAsync()
    {
        var slides = await _db.HomeSlides.Where(s => s.IsActive).ToListAsync();
        var testimonials = await _db.Testimonials.Where(t => t.IsActive).ToListAsync();
        var faqs = await _db.FAQs.Where(f => f.IsActive).ToListAsync();
        var categories = await _db.CourseCategories.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync();
        var topCourses = await _db.Courses.Include(c => c.Instructor).Include(c => c.Category)
            .OrderByDescending(c => c.CreatedAt).Take(4).ToListAsync();
        var latestPosts = await _db.BlogPosts.Where(p => p.IsActive).OrderByDescending(p => p.PublishedAt).Take(3).ToListAsync();
        var stories = await _db.StoryHighlights.Where(s => s.IsActive && s.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(s => s.CreatedAt).Take(10).ToListAsync();

        return new HomeDataDto
        {
            Slides = slides,
            Testimonials = testimonials,
            Faqs = faqs,
            Categories = categories,
            TopCourses = topCourses,
            LatestPosts = latestPosts,
            Stories = stories
        };
    }

    public async Task<(IEnumerable<BlogPost> Items, int Total)> GetBlogPostsAsync(int page, int pageSize)
    {
        var query = _db.BlogPosts.OrderByDescending(b => b.PublishedAt);
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }

    public Task<BlogPost?> GetBlogPostBySlugAsync(string slug) =>
        _db.BlogPosts.FirstOrDefaultAsync(b => b.Slug == slug);

    public async Task<IEnumerable<CourseCategory>> GetCategoriesAsync() =>
        await _db.CourseCategories.ToListAsync();

    public async Task<CourseCategory> AddCategoryAsync(CourseCategory category)
    {
        _db.CourseCategories.Add(category);
        await _db.SaveChangesAsync();
        return category;
    }

    public Task<CourseCategory?> FindCategoryAsync(Guid id) =>
        _db.CourseCategories.FindAsync(id).AsTask();

    public async Task UpdateCategoryAsync(CourseCategory category)
    {
        _db.CourseCategories.Update(category);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(CourseCategory category)
    {
        _db.CourseCategories.Remove(category);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Instructor>> GetInstructorsAsync() =>
        await _db.Instructors.ToListAsync();

    public async Task<Instructor> AddInstructorAsync(Instructor instructor)
    {
        _db.Instructors.Add(instructor);
        await _db.SaveChangesAsync();
        return instructor;
    }

    public Task<Instructor?> FindInstructorAsync(Guid id) =>
        _db.Instructors.FindAsync(id).AsTask();

    public async Task UpdateInstructorAsync(Instructor instructor)
    {
        _db.Instructors.Update(instructor);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteInstructorAsync(Instructor instructor)
    {
        _db.Instructors.Remove(instructor);
        await _db.SaveChangesAsync();
    }

    public async Task<BlogPost> AddBlogPostAsync(BlogPost post)
    {
        _db.BlogPosts.Add(post);
        await _db.SaveChangesAsync();
        return post;
    }

    public Task<BlogPost?> FindBlogPostAsync(Guid id) =>
        _db.BlogPosts.FindAsync(id).AsTask();

    public async Task UpdateBlogPostAsync(BlogPost post)
    {
        _db.BlogPosts.Update(post);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteBlogPostAsync(BlogPost post)
    {
        _db.BlogPosts.Remove(post);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<HomeSlide>> GetSlidesAsync() =>
        await _db.HomeSlides.ToListAsync();

    public async Task<HomeSlide> AddSlideAsync(HomeSlide slide)
    {
        _db.HomeSlides.Add(slide);
        await _db.SaveChangesAsync();
        return slide;
    }

    public Task<HomeSlide?> FindSlideAsync(Guid id) =>
        _db.HomeSlides.FindAsync(id).AsTask();

    public async Task UpdateSlideAsync(HomeSlide slide)
    {
        _db.HomeSlides.Update(slide);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteSlideAsync(HomeSlide slide)
    {
        _db.HomeSlides.Remove(slide);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Testimonial>> GetTestimonialsAsync() =>
        await _db.Testimonials.ToListAsync();

    public async Task<Testimonial> AddTestimonialAsync(Testimonial testimonial)
    {
        _db.Testimonials.Add(testimonial);
        await _db.SaveChangesAsync();
        return testimonial;
    }

    public Task<Testimonial?> FindTestimonialAsync(Guid id) =>
        _db.Testimonials.FindAsync(id).AsTask();

    public async Task UpdateTestimonialAsync(Testimonial testimonial)
    {
        _db.Testimonials.Update(testimonial);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteTestimonialAsync(Testimonial testimonial)
    {
        _db.Testimonials.Remove(testimonial);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<FAQ>> GetFaqsAsync() =>
        await _db.FAQs.ToListAsync();

    public async Task<FAQ> AddFaqAsync(FAQ faq)
    {
        _db.FAQs.Add(faq);
        await _db.SaveChangesAsync();
        return faq;
    }

    public Task<FAQ?> FindFaqAsync(Guid id) =>
        _db.FAQs.FindAsync(id).AsTask();

    public async Task UpdateFaqAsync(FAQ faq)
    {
        _db.FAQs.Update(faq);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteFaqAsync(FAQ faq)
    {
        _db.FAQs.Remove(faq);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<StoryHighlight>> GetStoriesAsync() =>
        await _db.StoryHighlights.ToListAsync();

    public async Task<StoryHighlight> AddStoryAsync(StoryHighlight story)
    {
        _db.StoryHighlights.Add(story);
        await _db.SaveChangesAsync();
        return story;
    }

    public Task<StoryHighlight?> FindStoryAsync(Guid id) =>
        _db.StoryHighlights.FindAsync(id).AsTask();

    public async Task UpdateStoryAsync(StoryHighlight story)
    {
        _db.StoryHighlights.Update(story);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteStoryAsync(StoryHighlight story)
    {
        _db.StoryHighlights.Remove(story);
        await _db.SaveChangesAsync();
    }
}
