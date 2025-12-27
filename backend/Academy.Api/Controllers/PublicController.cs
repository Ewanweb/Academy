using Academy.Api.Application.DTOs;
using Academy.Api.Application.Features.Home.Queries;
using Academy.Api.DTOs;
using Academy.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PublicController : ControllerBase
{
    private readonly IMediator _mediator;

    public PublicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("home")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<HomeDataDto>>> HomeData()
    {
        var data = await _mediator.Send(new GetHomeDataQuery());
        return Ok(ApiResponse<HomeDataDto>.Success(data, "داده‌های صفحه اصلی"));
    }
}
