using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Courses.Queries;

public class SearchCoursesQueryHandler : IRequestHandler<SearchCoursesQuery, (IEnumerable<Course> Items, int Total)>
{
    private readonly ICourseRepository _courses;

    public SearchCoursesQueryHandler(ICourseRepository courses)
    {
        _courses = courses;
    }

    public Task<(IEnumerable<Course> Items, int Total)> Handle(SearchCoursesQuery request, CancellationToken cancellationToken)
    {
        return _courses.SearchAsync(request.Q, request.CategoryId, request.Level, request.Mode, request.MinPrice, request.MaxPrice, request.Page, request.PageSize);
    }
}
