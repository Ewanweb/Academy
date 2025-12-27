using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Blog.Queries;

public class GetBlogPostsQueryHandler : IRequestHandler<GetBlogPostsQuery, (IEnumerable<BlogPost> Items, int Total)>
{
    private readonly IContentRepository _content;

    public GetBlogPostsQueryHandler(IContentRepository content)
    {
        _content = content;
    }

    public Task<(IEnumerable<BlogPost> Items, int Total)> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken) =>
        _content.GetBlogPostsAsync(request.Page, request.PageSize);
}
