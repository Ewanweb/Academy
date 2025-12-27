using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.DTOs;
using Academy.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("api/v1/admin/content")]
[Authorize(Roles = "Admin")]
public class AdminContentController : ControllerBase
{
    private readonly IContentRepository _content;

    public AdminContentController(IContentRepository content)
    {
        _content = content;
    }

    #region Stories
    [HttpGet("stories")]
    public async Task<ActionResult<ApiResponse<IEnumerable<StoryHighlight>>>> Stories()
    {
        var data = await _content.GetStoriesAsync();
        return Ok(ApiResponse<IEnumerable<StoryHighlight>>.Success(data, "استوری‌ها"));
    }

    [HttpPost("stories")]
    public async Task<ActionResult<ApiResponse<StoryHighlight>>> CreateStory(StoryHighlight story)
    {
        story.Id = Guid.NewGuid();
        story.CreatedAt = DateTime.UtcNow;
        story.UpdatedAt = DateTime.UtcNow;
        await _content.AddStoryAsync(story);
        return Ok(ApiResponse<StoryHighlight>.Success(story, "استوری ایجاد شد"));
    }

    [HttpPut("stories/{id:guid}")]
    public async Task<ActionResult<ApiResponse<StoryHighlight>>> UpdateStory(Guid id, StoryHighlight input)
    {
        var entity = await _content.FindStoryAsync(id);
        if (entity is null) return NotFound(ApiResponse<StoryHighlight>.Fail("استوری یافت نشد"));
        entity.Title = input.Title;
        entity.ImageUrl = input.ImageUrl;
        entity.Link = input.Link;
        entity.ExpiresAt = input.ExpiresAt;
        entity.IsActive = input.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _content.UpdateStoryAsync(entity);
        return Ok(ApiResponse<StoryHighlight>.Success(entity, "استوری به‌روز شد"));
    }

    [HttpDelete("stories/{id:guid}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteStory(Guid id)
    {
        var entity = await _content.FindStoryAsync(id);
        if (entity is null) return NotFound(ApiResponse<string>.Fail("استوری یافت نشد"));
        await _content.DeleteStoryAsync(entity);
        return Ok(ApiResponse<string>.Success("ok", "استوری حذف شد"));
    }
    #endregion

    #region Categories
    [HttpGet("categories")]
    public async Task<ActionResult<ApiResponse<IEnumerable<CourseCategory>>>> Categories()
    {
        var data = await _content.GetCategoriesAsync();
        return Ok(ApiResponse<IEnumerable<CourseCategory>>.Success(data, "دسته‌بندی‌ها"));
    }

    [HttpPost("categories")]
    public async Task<ActionResult<ApiResponse<CourseCategory>>> CreateCategory(CourseCategory category)
    {
        category.Id = Guid.NewGuid();
        category.CreatedAt = DateTime.UtcNow;
        category.UpdatedAt = DateTime.UtcNow;
        await _content.AddCategoryAsync(category);
        return Ok(ApiResponse<CourseCategory>.Success(category, "دسته‌بندی ایجاد شد"));
    }

    [HttpPut("categories/{id:guid}")]
    public async Task<ActionResult<ApiResponse<CourseCategory>>> UpdateCategory(Guid id, CourseCategory input)
    {
        var entity = await _content.FindCategoryAsync(id);
        if (entity is null) return NotFound(ApiResponse<CourseCategory>.Fail("دسته‌بندی یافت نشد"));
        entity.Name = input.Name;
        entity.Description = input.Description;
        entity.IsActive = input.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _content.UpdateCategoryAsync(entity);
        return Ok(ApiResponse<CourseCategory>.Success(entity, "دسته‌بندی به‌روز شد"));
    }

    [HttpDelete("categories/{id:guid}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteCategory(Guid id)
    {
        var entity = await _content.FindCategoryAsync(id);
        if (entity is null) return NotFound(ApiResponse<string>.Fail("دسته‌بندی یافت نشد"));
        await _content.DeleteCategoryAsync(entity);
        return Ok(ApiResponse<string>.Success("ok", "دسته‌بندی حذف شد"));
    }
    #endregion

    #region Instructors
    [HttpGet("instructors")]
    public async Task<ActionResult<ApiResponse<IEnumerable<Instructor>>>> Instructors()
    {
        var data = await _content.GetInstructorsAsync();
        return Ok(ApiResponse<IEnumerable<Instructor>>.Success(data, "مدرس‌ها"));
    }

    [HttpPost("instructors")]
    public async Task<ActionResult<ApiResponse<Instructor>>> CreateInstructor(Instructor instructor)
    {
        instructor.Id = Guid.NewGuid();
        instructor.CreatedAt = DateTime.UtcNow;
        instructor.UpdatedAt = DateTime.UtcNow;
        await _content.AddInstructorAsync(instructor);
        return Ok(ApiResponse<Instructor>.Success(instructor, "مدرس ایجاد شد"));
    }

    [HttpPut("instructors/{id:guid}")]
    public async Task<ActionResult<ApiResponse<Instructor>>> UpdateInstructor(Guid id, Instructor input)
    {
        var entity = await _content.FindInstructorAsync(id);
        if (entity is null) return NotFound(ApiResponse<Instructor>.Fail("مدرس یافت نشد"));
        entity.FullName = input.FullName;
        entity.Bio = input.Bio;
        entity.Expertise = input.Expertise;
        entity.AvatarUrl = input.AvatarUrl;
        entity.IsActive = input.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _content.UpdateInstructorAsync(entity);
        return Ok(ApiResponse<Instructor>.Success(entity, "مدرس به‌روز شد"));
    }

    [HttpDelete("instructors/{id:guid}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteInstructor(Guid id)
    {
        var entity = await _content.FindInstructorAsync(id);
        if (entity is null) return NotFound(ApiResponse<string>.Fail("مدرس یافت نشد"));
        await _content.DeleteInstructorAsync(entity);
        return Ok(ApiResponse<string>.Success("ok", "مدرس حذف شد"));
    }
    #endregion

    #region Blog
    [HttpPost("blog")]
    public async Task<ActionResult<ApiResponse<BlogPost>>> CreatePost(BlogPost post)
    {
        post.Id = Guid.NewGuid();
        post.CreatedAt = DateTime.UtcNow;
        post.UpdatedAt = DateTime.UtcNow;
        post.PublishedAt = DateTime.UtcNow;
        await _content.AddBlogPostAsync(post);
        return Ok(ApiResponse<BlogPost>.Success(post, "مقاله ایجاد شد"));
    }

    [HttpPut("blog/{id:guid}")]
    public async Task<ActionResult<ApiResponse<BlogPost>>> UpdatePost(Guid id, BlogPost input)
    {
        var entity = await _content.FindBlogPostAsync(id);
        if (entity is null) return NotFound(ApiResponse<BlogPost>.Fail("مقاله یافت نشد"));
        entity.Title = input.Title;
        entity.Slug = input.Slug;
        entity.Summary = input.Summary;
        entity.Content = input.Content;
        entity.Author = input.Author;
        entity.IsActive = input.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _content.UpdateBlogPostAsync(entity);
        return Ok(ApiResponse<BlogPost>.Success(entity, "مقاله به‌روز شد"));
    }

    [HttpDelete("blog/{id:guid}")]
    public async Task<ActionResult<ApiResponse<string>>> DeletePost(Guid id)
    {
        var entity = await _content.FindBlogPostAsync(id);
        if (entity is null) return NotFound(ApiResponse<string>.Fail("مقاله یافت نشد"));
        await _content.DeleteBlogPostAsync(entity);
        return Ok(ApiResponse<string>.Success("ok", "مقاله حذف شد"));
    }
    #endregion

    #region Slides
    [HttpGet("slides")]
    public async Task<ActionResult<ApiResponse<IEnumerable<HomeSlide>>>> Slides()
    {
        var slides = await _content.GetSlidesAsync();
        return Ok(ApiResponse<IEnumerable<HomeSlide>>.Success(slides, "اسلایدها"));
    }

    [HttpPost("slides")]
    public async Task<ActionResult<ApiResponse<HomeSlide>>> CreateSlide(HomeSlide slide)
    {
        slide.Id = Guid.NewGuid();
        slide.CreatedAt = DateTime.UtcNow;
        slide.UpdatedAt = DateTime.UtcNow;
        await _content.AddSlideAsync(slide);
        return Ok(ApiResponse<HomeSlide>.Success(slide, "اسلاید ایجاد شد"));
    }

    [HttpPut("slides/{id:guid}")]
    public async Task<ActionResult<ApiResponse<HomeSlide>>> UpdateSlide(Guid id, HomeSlide input)
    {
        var entity = await _content.FindSlideAsync(id);
        if (entity is null) return NotFound(ApiResponse<HomeSlide>.Fail("اسلاید یافت نشد"));
        entity.Title = input.Title;
        entity.Subtitle = input.Subtitle;
        entity.ImageUrl = input.ImageUrl;
        entity.ButtonText = input.ButtonText;
        entity.ButtonLink = input.ButtonLink;
        entity.IsActive = input.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _content.UpdateSlideAsync(entity);
        return Ok(ApiResponse<HomeSlide>.Success(entity, "اسلاید به‌روز شد"));
    }

    [HttpDelete("slides/{id:guid}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteSlide(Guid id)
    {
        var entity = await _content.FindSlideAsync(id);
        if (entity is null) return NotFound(ApiResponse<string>.Fail("اسلاید یافت نشد"));
        await _content.DeleteSlideAsync(entity);
        return Ok(ApiResponse<string>.Success("ok", "اسلاید حذف شد"));
    }
    #endregion

    #region Testimonials & FAQs
    [HttpGet("testimonials")]
    public async Task<ActionResult<ApiResponse<IEnumerable<Testimonial>>>> Testimonials()
    {
        var data = await _content.GetTestimonialsAsync();
        return Ok(ApiResponse<IEnumerable<Testimonial>>.Success(data, "نظرات هنرجویان"));
    }

    [HttpPost("testimonials")]
    public async Task<ActionResult<ApiResponse<Testimonial>>> CreateTestimonial(Testimonial testimonial)
    {
        testimonial.Id = Guid.NewGuid();
        testimonial.CreatedAt = DateTime.UtcNow;
        testimonial.UpdatedAt = DateTime.UtcNow;
        await _content.AddTestimonialAsync(testimonial);
        return Ok(ApiResponse<Testimonial>.Success(testimonial, "نظر ثبت شد"));
    }

    [HttpPut("testimonials/{id:guid}")]
    public async Task<ActionResult<ApiResponse<Testimonial>>> UpdateTestimonial(Guid id, Testimonial input)
    {
        var entity = await _content.FindTestimonialAsync(id);
        if (entity is null) return NotFound(ApiResponse<Testimonial>.Fail("نظر یافت نشد"));
        entity.StudentName = input.StudentName;
        entity.Message = input.Message;
        entity.Rating = input.Rating;
        entity.IsActive = input.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _content.UpdateTestimonialAsync(entity);
        return Ok(ApiResponse<Testimonial>.Success(entity, "نظر به‌روز شد"));
    }

    [HttpDelete("testimonials/{id:guid}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteTestimonial(Guid id)
    {
        var entity = await _content.FindTestimonialAsync(id);
        if (entity is null) return NotFound(ApiResponse<string>.Fail("نظر یافت نشد"));
        await _content.DeleteTestimonialAsync(entity);
        return Ok(ApiResponse<string>.Success("ok", "نظر حذف شد"));
    }

    [HttpGet("faqs")]
    public async Task<ActionResult<ApiResponse<IEnumerable<FAQ>>>> Faqs()
    {
        var data = await _content.GetFaqsAsync();
        return Ok(ApiResponse<IEnumerable<FAQ>>.Success(data, "سوالات متداول"));
    }

    [HttpPost("faqs")]
    public async Task<ActionResult<ApiResponse<FAQ>>> CreateFaq(FAQ faq)
    {
        faq.Id = Guid.NewGuid();
        faq.CreatedAt = DateTime.UtcNow;
        faq.UpdatedAt = DateTime.UtcNow;
        await _content.AddFaqAsync(faq);
        return Ok(ApiResponse<FAQ>.Success(faq, "سوال ثبت شد"));
    }

    [HttpPut("faqs/{id:guid}")]
    public async Task<ActionResult<ApiResponse<FAQ>>> UpdateFaq(Guid id, FAQ input)
    {
        var entity = await _content.FindFaqAsync(id);
        if (entity is null) return NotFound(ApiResponse<FAQ>.Fail("سوال یافت نشد"));
        entity.Question = input.Question;
        entity.Answer = input.Answer;
        entity.IsActive = input.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _content.UpdateFaqAsync(entity);
        return Ok(ApiResponse<FAQ>.Success(entity, "سوال به‌روز شد"));
    }

    [HttpDelete("faqs/{id:guid}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteFaq(Guid id)
    {
        var entity = await _content.FindFaqAsync(id);
        if (entity is null) return NotFound(ApiResponse<string>.Fail("سوال یافت نشد"));
        await _content.DeleteFaqAsync(entity);
        return Ok(ApiResponse<string>.Success("ok", "سوال حذف شد"));
    }
    #endregion
}
