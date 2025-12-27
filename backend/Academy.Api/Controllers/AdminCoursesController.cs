using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.DTOs;
using Academy.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("api/v1/admin/courses")]
[Authorize(Roles = "Admin")]
public class AdminCoursesController : ControllerBase
{
    private readonly ICourseRepository _courses;

    public AdminCoursesController(ICourseRepository courses)
    {
        _courses = courses;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<Course>>>> Get()
    {
        var data = await _courses.GetAllAdminAsync();
        return Ok(ApiResponse<IEnumerable<Course>>.Success(data, "لیست دوره‌ها"));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Course>>> Create(CourseCreateDto dto)
    {
        var entity = new Course
        {
            Title = dto.Title,
            Slug = dto.Slug,
            Description = dto.Description,
            Level = dto.Level,
            Mode = dto.Mode,
            Price = dto.Price,
            DurationWeeks = dto.DurationWeeks,
            Prerequisites = dto.Prerequisites,
            Syllabus = dto.Syllabus,
            CategoryId = dto.CategoryId,
            InstructorId = dto.InstructorId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };
        await _courses.CreateAsync(entity);
        return Ok(ApiResponse<Course>.Success(entity, "دوره ایجاد شد"));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<Course>>> Update(Guid id, CourseCreateDto dto)
    {
        var course = await _courses.FindByIdAsync(id);
        if (course is null) return NotFound(ApiResponse<Course>.Fail("دوره یافت نشد"));

        course.Title = dto.Title;
        course.Slug = dto.Slug;
        course.Description = dto.Description;
        course.Level = dto.Level;
        course.Mode = dto.Mode;
        course.Price = dto.Price;
        course.DurationWeeks = dto.DurationWeeks;
        course.Prerequisites = dto.Prerequisites;
        course.Syllabus = dto.Syllabus;
        course.CategoryId = dto.CategoryId;
        course.InstructorId = dto.InstructorId;
        course.UpdatedAt = DateTime.UtcNow;

        await _courses.UpdateAsync(course);
        return Ok(ApiResponse<Course>.Success(course, "دوره به‌روزرسانی شد"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(Guid id)
    {
        var course = await _courses.FindByIdAsync(id);
        if (course is null) return NotFound(ApiResponse<string>.Fail("دوره یافت نشد"));
        await _courses.DeleteAsync(course);
        return Ok(ApiResponse<string>.Success("ok", "دوره حذف شد"));
    }
}
