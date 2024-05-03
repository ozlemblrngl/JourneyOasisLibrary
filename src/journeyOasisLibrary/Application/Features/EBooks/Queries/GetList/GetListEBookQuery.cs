using Application.Features.EBooks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.EBooks.Constants.EBooksOperationClaims;

namespace Application.Features.EBooks.Queries.GetList;

public class GetListEBookQuery : IRequest<GetListResponse<GetListEBookListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListEBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetEBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListEBookQueryHandler : IRequestHandler<GetListEBookQuery, GetListResponse<GetListEBookListItemDto>>
    {
        private readonly IEBookRepository _eBookRepository;
        private readonly IMapper _mapper;

        public GetListEBookQueryHandler(IEBookRepository eBookRepository, IMapper mapper)
        {
            _eBookRepository = eBookRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListEBookListItemDto>> Handle(GetListEBookQuery request, CancellationToken cancellationToken)
        {
            IPaginate<EBook> eBooks = await _eBookRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListEBookListItemDto> response = _mapper.Map<GetListResponse<GetListEBookListItemDto>>(eBooks);
            return response;
        }
    }
}