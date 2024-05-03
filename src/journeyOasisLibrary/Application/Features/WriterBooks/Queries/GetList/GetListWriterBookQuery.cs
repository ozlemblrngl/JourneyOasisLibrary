using Application.Features.WriterBooks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.WriterBooks.Constants.WriterBooksOperationClaims;

namespace Application.Features.WriterBooks.Queries.GetList;

public class GetListWriterBookQuery : IRequest<GetListResponse<GetListWriterBookListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListWriterBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetWriterBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListWriterBookQueryHandler : IRequestHandler<GetListWriterBookQuery, GetListResponse<GetListWriterBookListItemDto>>
    {
        private readonly IWriterBookRepository _writerBookRepository;
        private readonly IMapper _mapper;

        public GetListWriterBookQueryHandler(IWriterBookRepository writerBookRepository, IMapper mapper)
        {
            _writerBookRepository = writerBookRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListWriterBookListItemDto>> Handle(GetListWriterBookQuery request, CancellationToken cancellationToken)
        {
            IPaginate<WriterBook> writerBooks = await _writerBookRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWriterBookListItemDto> response = _mapper.Map<GetListResponse<GetListWriterBookListItemDto>>(writerBooks);
            return response;
        }
    }
}