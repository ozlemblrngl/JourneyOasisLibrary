using Application.Features.BookFormats.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.BookFormats.Constants.BookFormatsOperationClaims;

namespace Application.Features.BookFormats.Queries.GetList;

public class GetListBookFormatQuery : IRequest<GetListResponse<GetListBookFormatListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBookFormats({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBookFormats";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBookFormatQueryHandler : IRequestHandler<GetListBookFormatQuery, GetListResponse<GetListBookFormatListItemDto>>
    {
        private readonly IBookFormatRepository _bookFormatRepository;
        private readonly IMapper _mapper;

        public GetListBookFormatQueryHandler(IBookFormatRepository bookFormatRepository, IMapper mapper)
        {
            _bookFormatRepository = bookFormatRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBookFormatListItemDto>> Handle(GetListBookFormatQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BookFormat> bookFormats = await _bookFormatRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBookFormatListItemDto> response = _mapper.Map<GetListResponse<GetListBookFormatListItemDto>>(bookFormats);
            return response;
        }
    }
}