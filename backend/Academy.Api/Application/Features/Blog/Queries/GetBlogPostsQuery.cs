using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Blog.Queries;

public record GetBlogPostsQuery(int Page, int PageSize) : IRequest<(IEnumerable<BlogPost> Items, int Total)>;
