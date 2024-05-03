using Application.Features.Subjects.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Subjects.Constants.SubjectsOperationClaims;

namespace Application.Features.Subjects.Queries.GetList;

public class GetListSubjectQuery : IRequest<GetListResponse<GetListSubjectListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListSubjects({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetSubjects";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSubjectQueryHandler : IRequestHandler<GetListSubjectQuery, GetListResponse<GetListSubjectListItemDto>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public GetListSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSubjectListItemDto>> Handle(GetListSubjectQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Subject> subjects = await _subjectRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSubjectListItemDto> response = _mapper.Map<GetListResponse<GetListSubjectListItemDto>>(subjects);
            return response;
        }
    }
}