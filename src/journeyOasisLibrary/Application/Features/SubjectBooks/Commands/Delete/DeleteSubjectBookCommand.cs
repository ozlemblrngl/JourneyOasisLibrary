using Application.Features.SubjectBooks.Constants;
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

namespace Application.Features.SubjectBooks.Commands.Delete;

public class DeleteSubjectBookCommand : IRequest<DeletedSubjectBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, SubjectBooksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSubjectBooks"];

    public class DeleteSubjectBookCommandHandler : IRequestHandler<DeleteSubjectBookCommand, DeletedSubjectBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectBookRepository _subjectBookRepository;
        private readonly SubjectBookBusinessRules _subjectBookBusinessRules;

        public DeleteSubjectBookCommandHandler(IMapper mapper, ISubjectBookRepository subjectBookRepository,
                                         SubjectBookBusinessRules subjectBookBusinessRules)
        {
            _mapper = mapper;
            _subjectBookRepository = subjectBookRepository;
            _subjectBookBusinessRules = subjectBookBusinessRules;
        }

        public async Task<DeletedSubjectBookResponse> Handle(DeleteSubjectBookCommand request, CancellationToken cancellationToken)
        {
            SubjectBook? subjectBook = await _subjectBookRepository.GetAsync(predicate: sb => sb.Id == request.Id, cancellationToken: cancellationToken);
            await _subjectBookBusinessRules.SubjectBookShouldExistWhenSelected(subjectBook);

            await _subjectBookRepository.DeleteAsync(subjectBook!);

            DeletedSubjectBookResponse response = _mapper.Map<DeletedSubjectBookResponse>(subjectBook);
            return response;
        }
    }
}