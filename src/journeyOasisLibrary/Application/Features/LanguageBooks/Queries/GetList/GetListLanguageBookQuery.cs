using Application.Features.LanguageBooks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.LanguageBooks.Constants.LanguageBooksOperationClaims;

namespace Application.Features.LanguageBooks.Queries.GetList;

public class GetListLanguageBookQuery : IRequest<GetListResponse<GetListLanguageBookListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListLanguageBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetLanguageBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLanguageBookQueryHandler : IRequestHandler<GetListLanguageBookQuery, GetListResponse<GetListLanguageBookListItemDto>>
    {
        private readonly ILanguageBookRepository _languageBookRepository;
        private readonly IMapper _mapper;

        public GetListLanguageBookQueryHandler(ILanguageBookRepository languageBookRepository, IMapper mapper)
        {
            _languageBookRepository = languageBookRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLanguageBookListItemDto>> Handle(GetListLanguageBookQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LanguageBook> languageBooks = await _languageBookRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLanguageBookListItemDto> response = _mapper.Map<GetListResponse<GetListLanguageBookListItemDto>>(languageBooks);
            return response;
        }
    }
}