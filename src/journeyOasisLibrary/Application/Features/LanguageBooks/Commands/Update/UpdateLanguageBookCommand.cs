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

namespace Application.Features.LanguageBooks.Commands.Update;

public class UpdateLanguageBookCommand : IRequest<UpdatedLanguageBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid LanguageId { get; set; }
    public Book? Book { get; set; }
    public Language? Language { get; set; }

    public string[] Roles => [Admin, Write, LanguageBooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLanguageBooks"];

    public class UpdateLanguageBookCommandHandler : IRequestHandler<UpdateLanguageBookCommand, UpdatedLanguageBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILanguageBookRepository _languageBookRepository;
        private readonly LanguageBookBusinessRules _languageBookBusinessRules;

        public UpdateLanguageBookCommandHandler(IMapper mapper, ILanguageBookRepository languageBookRepository,
                                         LanguageBookBusinessRules languageBookBusinessRules)
        {
            _mapper = mapper;
            _languageBookRepository = languageBookRepository;
            _languageBookBusinessRules = languageBookBusinessRules;
        }

        public async Task<UpdatedLanguageBookResponse> Handle(UpdateLanguageBookCommand request, CancellationToken cancellationToken)
        {
            LanguageBook? languageBook = await _languageBookRepository.GetAsync(predicate: lb => lb.Id == request.Id, cancellationToken: cancellationToken);
            await _languageBookBusinessRules.LanguageBookShouldExistWhenSelected(languageBook);
            languageBook = _mapper.Map(request, languageBook);

            await _languageBookRepository.UpdateAsync(languageBook!);

            UpdatedLanguageBookResponse response = _mapper.Map<UpdatedLanguageBookResponse>(languageBook);
            return response;
        }
    }
}