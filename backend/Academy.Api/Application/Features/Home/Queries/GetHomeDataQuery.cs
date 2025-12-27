using Academy.Api.Application.DTOs;
using MediatR;

namespace Academy.Api.Application.Features.Home.Queries;

public record GetHomeDataQuery : IRequest<HomeDataDto>;
