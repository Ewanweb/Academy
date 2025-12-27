using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Data;
using Academy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Api.Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AcademyDbContext _db;
    public AttendanceRepository(AcademyDbContext db) { _db = db; }

    public async Task<AttendanceSession> CreateSessionAsync(AttendanceSession session)
    {
        _db.AttendanceSessions.Add(session);
        await _db.SaveChangesAsync();
        return session;
    }

    public async Task<IEnumerable<AttendanceSession>> GetSessionsByCourseAsync(Guid courseId) =>
        await _db.AttendanceSessions.Where(s => s.CourseId == courseId).OrderByDescending(s => s.SessionDate).ToListAsync();

    public async Task<AttendanceRecord> AddRecordAsync(AttendanceRecord record)
    {
        _db.AttendanceRecords.Add(record);
        await _db.SaveChangesAsync();
        return record;
    }

    public async Task<IEnumerable<AttendanceRecord>> GetRecordsAsync(Guid sessionId) =>
        await _db.AttendanceRecords.Include(r => r.User).Where(r => r.AttendanceSessionId == sessionId).ToListAsync();
}
