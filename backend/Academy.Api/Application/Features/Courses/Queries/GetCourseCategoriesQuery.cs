using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Courses.Queries;

public record GetCourseCategoriesQuery : IRequest<IEnumerable<CourseCategory>>;
