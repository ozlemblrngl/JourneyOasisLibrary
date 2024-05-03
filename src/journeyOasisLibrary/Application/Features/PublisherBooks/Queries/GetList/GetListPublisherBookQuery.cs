using Application.Features.PublisherBooks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.PublisherBooks.Constants.PublisherBooksOperationClaims;

namespace Application.Features.PublisherBooks.Queries.GetList;

public class GetListPublisherBookQuery : IRequest<GetListResponse<GetListPublisherBookListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListPublisherBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetPublisherBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPublisherBookQueryHandler : IRequestHandler<GetListPublisherBookQuery, GetListResponse<GetListPublisherBookListItemDto>>
    {
        private readonly IPublisherBookRepository _publisherBookRepository;
        private readonly IMapper _mapper;

        public GetListPublisherBookQueryHandler(IPublisherBookRepository publisherBookRepository, IMapper mapper)
        {
            _publisherBookRepository = publisherBookRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPublisherBookListItemDto>> Handle(GetListPublisherBookQuery request, CancellationToken cancellationToken)
        {
            IPaginate<PublisherBook> publisherBooks = await _publisherBookRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPublisherBookListItemDto> response = _mapper.Map<GetListResponse<GetListPublisherBookListItemDto>>(publisherBooks);
            return response;
        }
    }
}