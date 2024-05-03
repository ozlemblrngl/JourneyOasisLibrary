using Application.Features.TranslatorBooks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.TranslatorBooks.Constants.TranslatorBooksOperationClaims;

namespace Application.Features.TranslatorBooks.Queries.GetList;

public class GetListTranslatorBookQuery : IRequest<GetListResponse<GetListTranslatorBookListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListTranslatorBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetTranslatorBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTranslatorBookQueryHandler : IRequestHandler<GetListTranslatorBookQuery, GetListResponse<GetListTranslatorBookListItemDto>>
    {
        private readonly ITranslatorBookRepository _translatorBookRepository;
        private readonly IMapper _mapper;

        public GetListTranslatorBookQueryHandler(ITranslatorBookRepository translatorBookRepository, IMapper mapper)
        {
            _translatorBookRepository = translatorBookRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTranslatorBookListItemDto>> Handle(GetListTranslatorBookQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TranslatorBook> translatorBooks = await _translatorBookRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTranslatorBookListItemDto> response = _mapper.Map<GetListResponse<GetListTranslatorBookListItemDto>>(translatorBooks);
            return response;
        }
    }
}