using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Enrollments.Commands;

public class UpdateEnrollmentStatusCommandHandler : IRequestHandler<UpdateEnrollmentStatusCommand, EnrollmentRequest>
{
    private readonly IEnrollmentRepository _enrollments;

    public UpdateEnrollmentStatusCommandHandler(IEnrollmentRepository enrollments)
    {
        _enrollments = enrollments;
    }

    public async Task<EnrollmentRequest> Handle(UpdateEnrollmentStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _enrollments.FindAsync(request.EnrollmentId) ?? throw new InvalidOperationException("EnrollmentNotFound");
        var valid = new[] { "Pending", "Approved", "Rejected" };
        if (!valid.Contains(request.Status)) throw new InvalidOperationException("InvalidStatus");

        entity.Status = request.Status;
        entity.UpdatedAt = DateTime.UtcNow;
        await _enrollments.UpdateAsync(entity);
        return entity;
    }
}
