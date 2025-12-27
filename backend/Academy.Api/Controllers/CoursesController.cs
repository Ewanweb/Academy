using Academy.Api.Application.Features.Courses.Queries;
using Academy.Api.DTOs;
using Academy.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<object>>> GetAll([FromQuery] string? q, [FromQuery] Guid? categoryId,
        [FromQuery] string? level, [FromQuery] string? mode, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await _mediator.Send(new SearchCoursesQuery(q, categoryId, level, mode, minPrice, maxPrice, page, pageSize));
        return Ok(ApiResponse<object>.Success(new { total, items }, "لیست دوره‌ها"));
    }

    [HttpGet("{slug}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<Course?>>> GetBySlug(string slug)
    {
        var course = await _mediator.Send(new GetCourseBySlugQuery(slug));
        if (course is null) return NotFound(ApiResponse<Course?>.Fail("دوره یافت نشد"));
        return Ok(ApiResponse<Course?>.Success(course, "جزئیات دوره"));
    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<CourseCategory>>>> GetCategories()
    {
        var categories = await _mediator.Send(new GetCourseCategoriesQuery());
        return Ok(ApiResponse<IEnumerable<CourseCategory>>.Success(categories, "دسته‌بندی‌ها"));
    }
}
