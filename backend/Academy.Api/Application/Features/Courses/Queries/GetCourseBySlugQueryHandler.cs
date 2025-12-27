using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Courses.Queries;

public class GetCourseBySlugQueryHandler : IRequestHandler<GetCourseBySlugQuery, Course?>
{
    private readonly ICourseRepository _courses;

    public GetCourseBySlugQueryHandler(ICourseRepository courses)
    {
        _courses = courses;
    }

    public Task<Course?> Handle(GetCourseBySlugQuery request, CancellationToken cancellationToken) =>
        _courses.GetBySlugAsync(request.Slug);
}
