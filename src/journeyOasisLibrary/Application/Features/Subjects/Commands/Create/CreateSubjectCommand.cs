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

namespace Application.Features.Subjects.Commands.Create;

public class CreateSubjectCommand : IRequest<CreatedSubjectResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => [Admin, Write, SubjectsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSubjects"];

    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, CreatedSubjectResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly SubjectBusinessRules _subjectBusinessRules;

        public CreateSubjectCommandHandler(IMapper mapper, ISubjectRepository subjectRepository,
                                         SubjectBusinessRules subjectBusinessRules)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
            _subjectBusinessRules = subjectBusinessRules;
        }

        public async Task<CreatedSubjectResponse> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            Subject subject = _mapper.Map<Subject>(request);

            await _subjectRepository.AddAsync(subject);

            CreatedSubjectResponse response = _mapper.Map<CreatedSubjectResponse>(subject);
            return response;
        }
    }
}