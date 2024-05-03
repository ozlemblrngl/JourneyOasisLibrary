using Application.Features.SubjectBooks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.SubjectBooks.Constants.SubjectBooksOperationClaims;

namespace Application.Features.SubjectBooks.Queries.GetList;

public class GetListSubjectBookQuery : IRequest<GetListResponse<GetListSubjectBookListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListSubjectBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetSubjectBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSubjectBookQueryHandler : IRequestHandler<GetListSubjectBookQuery, GetListResponse<GetListSubjectBookListItemDto>>
    {
        private readonly ISubjectBookRepository _subjectBookRepository;
        private readonly IMapper _mapper;

        public GetListSubjectBookQueryHandler(ISubjectBookRepository subjectBookRepository, IMapper mapper)
        {
            _subjectBookRepository = subjectBookRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSubjectBookListItemDto>> Handle(GetListSubjectBookQuery request, CancellationToken cancellationToken)
        {
            IPaginate<SubjectBook> subjectBooks = await _subjectBookRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSubjectBookListItemDto> response = _mapper.Map<GetListResponse<GetListSubjectBookListItemDto>>(subjectBooks);
            return response;
        }
    }
}