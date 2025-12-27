using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Data;
using Academy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Api.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AcademyDbContext _db;

    public EnrollmentRepository(AcademyDbContext db)
    {
        _db = db;
    }

    public async Task<EnrollmentRequest> CreateAsync(EnrollmentRequest request)
    {
        _db.EnrollmentRequests.Add(request);
        await _db.SaveChangesAsync();
        return request;
    }

    public async Task<IEnumerable<EnrollmentRequest>> GetByUserAsync(Guid userId) =>
        await _db.EnrollmentRequests.Include(e => e.Course).Where(e => e.UserId == userId).ToListAsync();

    public async Task<IEnumerable<EnrollmentRequest>> GetAllAsync() =>
        await _db.EnrollmentRequests.Include(e => e.Course).Include(e => e.User).ToListAsync();

    public Task<EnrollmentRequest?> FindAsync(Guid id) =>
        _db.EnrollmentRequests.FindAsync(id).AsTask();

    public async Task UpdateAsync(EnrollmentRequest enrollment)
    {
        _db.EnrollmentRequests.Update(enrollment);
        await _db.SaveChangesAsync();
    }
}
