using Application.Features.TranslatorBooks.Constants;
using Application.Features.TranslatorBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.TranslatorBooks.Constants.TranslatorBooksOperationClaims;

namespace Application.Features.TranslatorBooks.Commands.Create;

public class CreateTranslatorBookCommand : IRequest<CreatedTranslatorBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookId { get; set; }
    public Guid TranslatorId { get; set; }
    public Book? Book { get; set; }
    public Translator? Translator { get; set; }

    public string[] Roles => [Admin, Write, TranslatorBooksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTranslatorBooks"];

    public class CreateTranslatorBookCommandHandler : IRequestHandler<CreateTranslatorBookCommand, CreatedTranslatorBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITranslatorBookRepository _translatorBookRepository;
        private readonly TranslatorBookBusinessRules _translatorBookBusinessRules;

        public CreateTranslatorBookCommandHandler(IMapper mapper, ITranslatorBookRepository translatorBookRepository,
                                         TranslatorBookBusinessRules translatorBookBusinessRules)
        {
            _mapper = mapper;
            _translatorBookRepository = translatorBookRepository;
            _translatorBookBusinessRules = translatorBookBusinessRules;
        }

        public async Task<CreatedTranslatorBookResponse> Handle(CreateTranslatorBookCommand request, CancellationToken cancellationToken)
        {
            TranslatorBook translatorBook = _mapper.Map<TranslatorBook>(request);

            await _translatorBookRepository.AddAsync(translatorBook);

            CreatedTranslatorBookResponse response = _mapper.Map<CreatedTranslatorBookResponse>(translatorBook);
            return response;
        }
    }
}