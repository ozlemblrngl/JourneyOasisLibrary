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

namespace Application.Features.SubjectBooks.Commands.Update;

public class UpdateSubjectBookCommand : IRequest<UpdatedSubjectBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid SubjectId { get; set; }
    public Book? Book { get; set; }
    public Subject? Subject { get; set; }

    public string[] Roles => [Admin, Write, SubjectBooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSubjectBooks"];

    public class UpdateSubjectBookCommandHandler : IRequestHandler<UpdateSubjectBookCommand, UpdatedSubjectBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectBookRepository _subjectBookRepository;
        private readonly SubjectBookBusinessRules _subjectBookBusinessRules;

        public UpdateSubjectBookCommandHandler(IMapper mapper, ISubjectBookRepository subjectBookRepository,
                                         SubjectBookBusinessRules subjectBookBusinessRules)
        {
            _mapper = mapper;
            _subjectBookRepository = subjectBookRepository;
            _subjectBookBusinessRules = subjectBookBusinessRules;
        }

        public async Task<UpdatedSubjectBookResponse> Handle(UpdateSubjectBookCommand request, CancellationToken cancellationToken)
        {
            SubjectBook? subjectBook = await _subjectBookRepository.GetAsync(predicate: sb => sb.Id == request.Id, cancellationToken: cancellationToken);
            await _subjectBookBusinessRules.SubjectBookShouldExistWhenSelected(subjectBook);
            subjectBook = _mapper.Map(request, subjectBook);

            await _subjectBookRepository.UpdateAsync(subjectBook!);

            UpdatedSubjectBookResponse response = _mapper.Map<UpdatedSubjectBookResponse>(subjectBook);
            return response;
        }
    }
}