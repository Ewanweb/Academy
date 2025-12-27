using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Courses.Queries;

public class GetCourseCategoriesQueryHandler : IRequestHandler<GetCourseCategoriesQuery, IEnumerable<CourseCategory>>
{
    private readonly ICourseRepository _courses;

    public GetCourseCategoriesQueryHandler(ICourseRepository courses)
    {
        _courses = courses;
    }

    public Task<IEnumerable<CourseCategory>> Handle(GetCourseCategoriesQuery request, CancellationToken cancellationToken) =>
        _courses.GetCategoriesAsync();
}
