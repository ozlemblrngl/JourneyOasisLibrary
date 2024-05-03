using Application.Features.SubjectBooks.Constants;
using Application.Features.SubjectBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.SubjectBooks.Constants.SubjectBooksOperationClaims;

namespace Application.Features.SubjectBooks.Commands.Create;

public class CreateSubjectBookCommand : IRequest<CreatedSubjectBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookId { get; set; }
    public Guid SubjectId { get; set; }
    public Book? Book { get; set; }
    public Subject? Subject { get; set; }

    public string[] Roles => [Admin, Write, SubjectBooksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSubjectBooks"];

    public class CreateSubjectBookCommandHandler : IRequestHandler<CreateSubjectBookCommand, CreatedSubjectBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectBookRepository _subjectBookRepository;
        private readonly SubjectBookBusinessRules _subjectBookBusinessRules;

        public CreateSubjectBookCommandHandler(IMapper mapper, ISubjectBookRepository subjectBookRepository,
                                         SubjectBookBusinessRules subjectBookBusinessRules)
        {
            _mapper = mapper;
            _subjectBookRepository = subjectBookRepository;
            _subjectBookBusinessRules = subjectBookBusinessRules;
        }

        public async Task<CreatedSubjectBookResponse> Handle(CreateSubjectBookCommand request, CancellationToken cancellationToken)
        {
            SubjectBook subjectBook = _mapper.Map<SubjectBook>(request);

            await _subjectBookRepository.AddAsync(subjectBook);

            CreatedSubjectBookResponse response = _mapper.Map<CreatedSubjectBookResponse>(subjectBook);
            return response;
        }
    }
}