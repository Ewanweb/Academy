using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Courses.Queries;

public record SearchCoursesQuery(string? Q, Guid? CategoryId, string? Level, string? Mode, decimal? MinPrice, decimal? MaxPrice, int Page, int PageSize)
    : IRequest<(IEnumerable<Course> Items, int Total)>;
