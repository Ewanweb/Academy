using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Enrollments.Commands;

public record UpdateEnrollmentStatusCommand(Guid EnrollmentId, string Status) : IRequest<EnrollmentRequest>;
