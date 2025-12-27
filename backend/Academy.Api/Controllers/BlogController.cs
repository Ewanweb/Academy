using Academy.Api.Application.Features.Blog.Queries;
using Academy.Api.DTOs;
using Academy.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<object>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await _mediator.Send(new GetBlogPostsQuery(page, pageSize));
        return Ok(ApiResponse<object>.Success(new { total, items }, "لیست مقالات"));
    }

    [HttpGet("{slug}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<BlogPost?>>> GetBySlug(string slug)
    {
        var post = await _mediator.Send(new GetBlogBySlugQuery(slug));
        if (post is null) return NotFound(ApiResponse<BlogPost?>.Fail("مقاله یافت نشد"));
        return Ok(ApiResponse<BlogPost?>.Success(post, "جزئیات مقاله"));
    }
}
