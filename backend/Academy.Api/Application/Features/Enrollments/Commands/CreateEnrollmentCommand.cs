using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Enrollments.Commands;

public record CreateEnrollmentCommand(Guid CourseId, Guid UserId, string Note) : IRequest<EnrollmentRequest>;
