using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Blog.Queries;

public class GetBlogBySlugQueryHandler : IRequestHandler<GetBlogBySlugQuery, BlogPost?>
{
    private readonly IContentRepository _content;

    public GetBlogBySlugQueryHandler(IContentRepository content)
    {
        _content = content;
    }

    public Task<BlogPost?> Handle(GetBlogBySlugQuery request, CancellationToken cancellationToken) =>
        _content.GetBlogPostBySlugAsync(request.Slug);
}
