using Application.Features.TranslatorBooks.Constants;
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

namespace Application.Features.TranslatorBooks.Commands.Delete;

public class DeleteTranslatorBookCommand : IRequest<DeletedTranslatorBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, TranslatorBooksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTranslatorBooks"];

    public class DeleteTranslatorBookCommandHandler : IRequestHandler<DeleteTranslatorBookCommand, DeletedTranslatorBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITranslatorBookRepository _translatorBookRepository;
        private readonly TranslatorBookBusinessRules _translatorBookBusinessRules;

        public DeleteTranslatorBookCommandHandler(IMapper mapper, ITranslatorBookRepository translatorBookRepository,
                                         TranslatorBookBusinessRules translatorBookBusinessRules)
        {
            _mapper = mapper;
            _translatorBookRepository = translatorBookRepository;
            _translatorBookBusinessRules = translatorBookBusinessRules;
        }

        public async Task<DeletedTranslatorBookResponse> Handle(DeleteTranslatorBookCommand request, CancellationToken cancellationToken)
        {
            TranslatorBook? translatorBook = await _translatorBookRepository.GetAsync(predicate: tb => tb.Id == request.Id, cancellationToken: cancellationToken);
            await _translatorBookBusinessRules.TranslatorBookShouldExistWhenSelected(translatorBook);

            await _translatorBookRepository.DeleteAsync(translatorBook!);

            DeletedTranslatorBookResponse response = _mapper.Map<DeletedTranslatorBookResponse>(translatorBook);
            return response;
        }
    }
}