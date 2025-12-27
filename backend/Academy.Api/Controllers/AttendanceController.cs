using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.DTOs;
using Academy.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("api/v1/admin/attendance")]
[Authorize(Roles = "Admin")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceRepository _attendance;

    public AttendanceController(IAttendanceRepository attendance)
    {
        _attendance = attendance;
    }

    [HttpPost("sessions")]
    public async Task<ActionResult<ApiResponse<AttendanceSession>>> CreateSession(AttendanceSessionCreateDto dto)
    {
        var session = new AttendanceSession
        {
            CourseId = dto.CourseId,
            SessionDate = dto.SessionDate,
            Title = dto.Title,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await _attendance.CreateSessionAsync(session);
        return Ok(ApiResponse<AttendanceSession>.Success(session, "جلسه حضور و غیاب ایجاد شد"));
    }

    [HttpGet("sessions/{courseId:guid}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<AttendanceSession>>>> GetSessions(Guid courseId)
    {
        var sessions = await _attendance.GetSessionsByCourseAsync(courseId);
        return Ok(ApiResponse<IEnumerable<AttendanceSession>>.Success(sessions, "لیست جلسات"));
    }

    [HttpPost("records")]
    public async Task<ActionResult<ApiResponse<AttendanceRecord>>> AddRecord(AttendanceRecordCreateDto dto)
    {
        var record = new AttendanceRecord
        {
            AttendanceSessionId = dto.SessionId,
            UserId = dto.UserId,
            Status = dto.Status,
            Note = dto.Note,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await _attendance.AddRecordAsync(record);
        return Ok(ApiResponse<AttendanceRecord>.Success(record, "حضور/غیاب ثبت شد"));
    }

    [HttpGet("records/{sessionId:guid}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<AttendanceRecord>>>> GetRecords(Guid sessionId)
    {
        var records = await _attendance.GetRecordsAsync(sessionId);
        return Ok(ApiResponse<IEnumerable<AttendanceRecord>>.Success(records, "لیست حضور/غیاب"));
    }
}
