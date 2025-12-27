using Academy.Api.Models;
using MediatR;

namespace Academy.Api.Application.Features.Blog.Queries;

public record GetBlogBySlugQuery(string Slug) : IRequest<BlogPost?>;
