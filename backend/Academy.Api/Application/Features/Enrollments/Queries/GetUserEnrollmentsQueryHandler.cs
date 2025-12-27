using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Enrollments.Queries;

public class GetUserEnrollmentsQueryHandler : IRequestHandler<GetUserEnrollmentsQuery, IEnumerable<EnrollmentRequest>>
{
    private readonly IEnrollmentRepository _enrollments;

    public GetUserEnrollmentsQueryHandler(IEnrollmentRepository enrollments)
    {
        _enrollments = enrollments;
    }

    public Task<IEnumerable<EnrollmentRequest>> Handle(GetUserEnrollmentsQuery request, CancellationToken cancellationToken) =>
        _enrollments.GetByUserAsync(request.UserId);
}
