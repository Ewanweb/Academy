using Academy.Api.Models;

namespace Academy.Api.Application.Contracts.Repositories;

public interface IAttendanceRepository
{
    Task<AttendanceSession> CreateSessionAsync(AttendanceSession session);
    Task<IEnumerable<AttendanceSession>> GetSessionsByCourseAsync(Guid courseId);
    Task<AttendanceRecord> AddRecordAsync(AttendanceRecord record);
    Task<IEnumerable<AttendanceRecord>> GetRecordsAsync(Guid sessionId);
}
