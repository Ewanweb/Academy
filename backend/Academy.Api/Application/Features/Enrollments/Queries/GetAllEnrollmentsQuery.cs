using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Enrollments.Queries;

public record GetAllEnrollmentsQuery : IRequest<IEnumerable<EnrollmentRequest>>;
