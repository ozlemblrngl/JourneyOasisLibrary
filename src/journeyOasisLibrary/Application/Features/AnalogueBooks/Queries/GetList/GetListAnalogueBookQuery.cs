using Application.Features.AnalogueBooks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.AnalogueBooks.Constants.AnalogueBooksOperationClaims;

namespace Application.Features.AnalogueBooks.Queries.GetList;

public class GetListAnalogueBookQuery : IRequest<GetListResponse<GetListAnalogueBookListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListAnalogueBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAnalogueBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAnalogueBookQueryHandler : IRequestHandler<GetListAnalogueBookQuery, GetListResponse<GetListAnalogueBookListItemDto>>
    {
        private readonly IAnalogueBookRepository _analogueBookRepository;
        private readonly IMapper _mapper;

        public GetListAnalogueBookQueryHandler(IAnalogueBookRepository analogueBookRepository, IMapper mapper)
        {
            _analogueBookRepository = analogueBookRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAnalogueBookListItemDto>> Handle(GetListAnalogueBookQuery request, CancellationToken cancellationToken)
        {
            IPaginate<AnalogueBook> analogueBooks = await _analogueBookRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAnalogueBookListItemDto> response = _mapper.Map<GetListResponse<GetListAnalogueBookListItemDto>>(analogueBooks);
            return response;
        }
    }
}