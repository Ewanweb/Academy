using Academy.Api.Models;

namespace Academy.Api.Application.Contracts.Repositories;

public interface IEnrollmentRepository
{
    Task<EnrollmentRequest> CreateAsync(EnrollmentRequest request);
    Task<IEnumerable<EnrollmentRequest>> GetByUserAsync(Guid userId);
    Task<IEnumerable<EnrollmentRequest>> GetAllAsync();
    Task<EnrollmentRequest?> FindAsync(Guid id);
    Task UpdateAsync(EnrollmentRequest enrollment);
}
