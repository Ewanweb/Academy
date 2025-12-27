using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Enrollments.Queries;

public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, IEnumerable<EnrollmentRequest>>
{
    private readonly IEnrollmentRepository _enrollments;

    public GetAllEnrollmentsQueryHandler(IEnrollmentRepository enrollments)
    {
        _enrollments = enrollments;
    }

    public Task<IEnumerable<EnrollmentRequest>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken) =>
        _enrollments.GetAllAsync();
}
