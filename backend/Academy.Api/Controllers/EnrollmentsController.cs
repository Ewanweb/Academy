using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Academy.Api.Application.Features.Enrollments.Commands;
using Academy.Api.Application.Features.Enrollments.Queries;
using Academy.Api.DTOs;
using Academy.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class EnrollmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnrollmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<EnrollmentRequest>>> Create(EnrollmentCreateDto dto)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub || c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return Unauthorized(ApiResponse<EnrollmentRequest>.Fail("توکن نامعتبر است"));
        var userId = Guid.Parse(userIdClaim.Value);
        try
        {
            var entity = await _mediator.Send(new CreateEnrollmentCommand(dto.CourseId, userId, dto.Note));
            return Ok(ApiResponse<EnrollmentRequest>.Success(entity, "درخواست ثبت‌نام ثبت شد"));
        }
        catch (InvalidOperationException ex) when (ex.Message == "CourseNotFound")
        {
            return NotFound(ApiResponse<EnrollmentRequest>.Fail("دوره یافت نشد"));
        }
    }

    [HttpGet("me")]
    public async Task<ActionResult<ApiResponse<IEnumerable<EnrollmentRequest>>>> MyEnrollments()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub || c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return Unauthorized(ApiResponse<IEnumerable<EnrollmentRequest>>.Fail("توکن نامعتبر است"));
        var userId = Guid.Parse(userIdClaim.Value);
        var enrollments = await _mediator.Send(new GetUserEnrollmentsQuery(userId));
        return Ok(ApiResponse<IEnumerable<EnrollmentRequest>>.Success(enrollments, "درخواست‌های شما"));
    }
}
