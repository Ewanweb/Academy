using Academy.Api.Application.Features.Enrollments.Commands;
using Academy.Api.Application.Features.Enrollments.Queries;
using Academy.Api.DTOs;
using Academy.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("api/v1/admin/enrollments")]
[Authorize(Roles = "Admin")]
public class AdminEnrollmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminEnrollmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<EnrollmentRequest>>>> GetAll()
    {
        var items = await _mediator.Send(new GetAllEnrollmentsQuery());
        return Ok(ApiResponse<IEnumerable<EnrollmentRequest>>.Success(items, "درخواست‌های ثبت‌نام"));
    }

    [HttpPatch("{id:guid}/{status}")]
    public async Task<ActionResult<ApiResponse<EnrollmentRequest>>> UpdateStatus(Guid id, string status)
    {
        try
        {
            var entity = await _mediator.Send(new UpdateEnrollmentStatusCommand(id, status));
            return Ok(ApiResponse<EnrollmentRequest>.Success(entity, "وضعیت به‌روز شد"));
        }
        catch (InvalidOperationException ex) when (ex.Message == "EnrollmentNotFound")
        {
            return NotFound(ApiResponse<EnrollmentRequest>.Fail("درخواست یافت نشد"));
        }
        catch (InvalidOperationException ex) when (ex.Message == "InvalidStatus")
        {
            return BadRequest(ApiResponse<EnrollmentRequest>.Fail("وضعیت نامعتبر است"));
        }
    }
}
