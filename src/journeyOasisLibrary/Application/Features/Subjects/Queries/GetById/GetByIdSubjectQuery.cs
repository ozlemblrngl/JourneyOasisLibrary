using Application.Features.Subjects.Constants;
using Application.Features.Subjects.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Subjects.Constants.SubjectsOperationClaims;

namespace Application.Features.Subjects.Queries.GetById;

public class GetByIdSubjectQuery : IRequest<GetByIdSubjectResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdSubjectQueryHandler : IRequestHandler<GetByIdSubjectQuery, GetByIdSubjectResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly SubjectBusinessRules _subjectBusinessRules;

        public GetByIdSubjectQueryHandler(IMapper mapper, ISubjectRepository subjectRepository, SubjectBusinessRules subjectBusinessRules)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
            _subjectBusinessRules = subjectBusinessRules;
        }

        public async Task<GetByIdSubjectResponse> Handle(GetByIdSubjectQuery request, CancellationToken cancellationToken)
        {
            Subject? subject = await _subjectRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _subjectBusinessRules.SubjectShouldExistWhenSelected(subject);

            GetByIdSubjectResponse response = _mapper.Map<GetByIdSubjectResponse>(subject);
            return response;
        }
    }
}