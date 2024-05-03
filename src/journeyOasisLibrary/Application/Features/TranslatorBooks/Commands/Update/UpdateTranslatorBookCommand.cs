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

namespace Application.Features.TranslatorBooks.Commands.Update;

public class UpdateTranslatorBookCommand : IRequest<UpdatedTranslatorBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid TranslatorId { get; set; }
    public Book? Book { get; set; }
    public Translator? Translator { get; set; }

    public string[] Roles => [Admin, Write, TranslatorBooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTranslatorBooks"];

    public class UpdateTranslatorBookCommandHandler : IRequestHandler<UpdateTranslatorBookCommand, UpdatedTranslatorBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITranslatorBookRepository _translatorBookRepository;
        private readonly TranslatorBookBusinessRules _translatorBookBusinessRules;

        public UpdateTranslatorBookCommandHandler(IMapper mapper, ITranslatorBookRepository translatorBookRepository,
                                         TranslatorBookBusinessRules translatorBookBusinessRules)
        {
            _mapper = mapper;
            _translatorBookRepository = translatorBookRepository;
            _translatorBookBusinessRules = translatorBookBusinessRules;
        }

        public async Task<UpdatedTranslatorBookResponse> Handle(UpdateTranslatorBookCommand request, CancellationToken cancellationToken)
        {
            TranslatorBook? translatorBook = await _translatorBookRepository.GetAsync(predicate: tb => tb.Id == request.Id, cancellationToken: cancellationToken);
            await _translatorBookBusinessRules.TranslatorBookShouldExistWhenSelected(translatorBook);
            translatorBook = _mapper.Map(request, translatorBook);

            await _translatorBookRepository.UpdateAsync(translatorBook!);

            UpdatedTranslatorBookResponse response = _mapper.Map<UpdatedTranslatorBookResponse>(translatorBook);
            return response;
        }
    }
}