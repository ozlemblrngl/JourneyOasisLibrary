using Application.Features.Subjects.Constants;
using Application.Features.Subjects.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Subjects.Constants.SubjectsOperationClaims;

namespace Application.Features.Subjects.Commands.Update;

public class UpdateSubjectCommand : IRequest<UpdatedSubjectResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => [Admin, Write, SubjectsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSubjects"];

    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, UpdatedSubjectResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly SubjectBusinessRules _subjectBusinessRules;

        public UpdateSubjectCommandHandler(IMapper mapper, ISubjectRepository subjectRepository,
                                         SubjectBusinessRules subjectBusinessRules)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
            _subjectBusinessRules = subjectBusinessRules;
        }

        public async Task<UpdatedSubjectResponse> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            Subject? subject = await _subjectRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _subjectBusinessRules.SubjectShouldExistWhenSelected(subject);
            subject = _mapper.Map(request, subject);

            await _subjectRepository.UpdateAsync(subject!);

            UpdatedSubjectResponse response = _mapper.Map<UpdatedSubjectResponse>(subject);
            return response;
        }
    }
}