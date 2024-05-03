using Application.Features.LanguageBooks.Constants;
using Application.Features.LanguageBooks.Constants;
using Application.Features.LanguageBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LanguageBooks.Constants.LanguageBooksOperationClaims;

namespace Application.Features.LanguageBooks.Commands.Delete;

public class DeleteLanguageBookCommand : IRequest<DeletedLanguageBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, LanguageBooksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLanguageBooks"];

    public class DeleteLanguageBookCommandHandler : IRequestHandler<DeleteLanguageBookCommand, DeletedLanguageBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILanguageBookRepository _languageBookRepository;
        private readonly LanguageBookBusinessRules _languageBookBusinessRules;

        public DeleteLanguageBookCommandHandler(IMapper mapper, ILanguageBookRepository languageBookRepository,
                                         LanguageBookBusinessRules languageBookBusinessRules)
        {
            _mapper = mapper;
            _languageBookRepository = languageBookRepository;
            _languageBookBusinessRules = languageBookBusinessRules;
        }

        public async Task<DeletedLanguageBookResponse> Handle(DeleteLanguageBookCommand request, CancellationToken cancellationToken)
        {
            LanguageBook? languageBook = await _languageBookRepository.GetAsync(predicate: lb => lb.Id == request.Id, cancellationToken: cancellationToken);
            await _languageBookBusinessRules.LanguageBookShouldExistWhenSelected(languageBook);

            await _languageBookRepository.DeleteAsync(languageBook!);

            DeletedLanguageBookResponse response = _mapper.Map<DeletedLanguageBookResponse>(languageBook);
            return response;
        }
    }
}