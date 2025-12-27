using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Enrollments.Commands;

public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, EnrollmentRequest>
{
    private readonly IEnrollmentRepository _enrollments;
    private readonly ICourseRepository _courses;

    public CreateEnrollmentCommandHandler(IEnrollmentRepository enrollments, ICourseRepository courses)
    {
        _enrollments = enrollments;
        _courses = courses;
    }

    public async Task<EnrollmentRequest> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var course = await _courses.FindByIdAsync(request.CourseId);
        if (course is null) throw new InvalidOperationException("CourseNotFound");

        var entity = new EnrollmentRequest
        {
            CourseId = request.CourseId,
            UserId = request.UserId,
            Note = request.Note,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await _enrollments.CreateAsync(entity);
        return entity;
    }
}
