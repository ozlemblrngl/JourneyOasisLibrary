using Application.Features.Writers.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Writers.Constants.WritersOperationClaims;

namespace Application.Features.Writers.Queries.GetList;

public class GetListWriterQuery : IRequest<GetListResponse<GetListWriterListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListWriters({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetWriters";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListWriterQueryHandler : IRequestHandler<GetListWriterQuery, GetListResponse<GetListWriterListItemDto>>
    {
        private readonly IWriterRepository _writerRepository;
        private readonly IMapper _mapper;

        public GetListWriterQueryHandler(IWriterRepository writerRepository, IMapper mapper)
        {
            _writerRepository = writerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListWriterListItemDto>> Handle(GetListWriterQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Writer> writers = await _writerRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWriterListItemDto> response = _mapper.Map<GetListResponse<GetListWriterListItemDto>>(writers);
            return response;
        }
    }
}