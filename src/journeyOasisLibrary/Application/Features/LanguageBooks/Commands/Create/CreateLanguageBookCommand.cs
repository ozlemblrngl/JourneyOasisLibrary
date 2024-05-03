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

namespace Application.Features.LanguageBooks.Commands.Create;

public class CreateLanguageBookCommand : IRequest<CreatedLanguageBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookId { get; set; }
    public Guid LanguageId { get; set; }
    public Book? Book { get; set; }
    public Language? Language { get; set; }

    public string[] Roles => [Admin, Write, LanguageBooksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLanguageBooks"];

    public class CreateLanguageBookCommandHandler : IRequestHandler<CreateLanguageBookCommand, CreatedLanguageBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILanguageBookRepository _languageBookRepository;
        private readonly LanguageBookBusinessRules _languageBookBusinessRules;

        public CreateLanguageBookCommandHandler(IMapper mapper, ILanguageBookRepository languageBookRepository,
                                         LanguageBookBusinessRules languageBookBusinessRules)
        {
            _mapper = mapper;
            _languageBookRepository = languageBookRepository;
            _languageBookBusinessRules = languageBookBusinessRules;
        }

        public async Task<CreatedLanguageBookResponse> Handle(CreateLanguageBookCommand request, CancellationToken cancellationToken)
        {
            LanguageBook languageBook = _mapper.Map<LanguageBook>(request);

            await _languageBookRepository.AddAsync(languageBook);

            CreatedLanguageBookResponse response = _mapper.Map<CreatedLanguageBookResponse>(languageBook);
            return response;
        }
    }
}