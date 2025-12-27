using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Application.DTOs;
using MediatR;

namespace Academy.Api.Application.Features.Home.Queries;

public class GetHomeDataQueryHandler : IRequestHandler<GetHomeDataQuery, HomeDataDto>
{
    private readonly IContentRepository _content;

    public GetHomeDataQueryHandler(IContentRepository content)
    {
        _content = content;
    }

    public async Task<HomeDataDto> Handle(GetHomeDataQuery request, CancellationToken cancellationToken)
    {
        return await _content.GetHomeDataAsync();
    }
}
