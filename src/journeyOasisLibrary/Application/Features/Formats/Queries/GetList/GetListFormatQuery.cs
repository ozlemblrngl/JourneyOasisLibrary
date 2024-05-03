using Application.Features.Formats.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Formats.Constants.FormatsOperationClaims;

namespace Application.Features.Formats.Queries.GetList;

public class GetListFormatQuery : IRequest<GetListResponse<GetListFormatListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListFormats({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetFormats";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListFormatQueryHandler : IRequestHandler<GetListFormatQuery, GetListResponse<GetListFormatListItemDto>>
    {
        private readonly IFormatRepository _formatRepository;
        private readonly IMapper _mapper;

        public GetListFormatQueryHandler(IFormatRepository formatRepository, IMapper mapper)
        {
            _formatRepository = formatRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFormatListItemDto>> Handle(GetListFormatQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Format> formats = await _formatRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFormatListItemDto> response = _mapper.Map<GetListResponse<GetListFormatListItemDto>>(formats);
            return response;
        }
    }
}