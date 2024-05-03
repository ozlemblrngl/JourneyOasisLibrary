using Application.Features.TranslatorBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.TranslatorBooks.Rules;

public class TranslatorBookBusinessRules : BaseBusinessRules
{
    private readonly ITranslatorBookRepository _translatorBookRepository;
    private readonly ILocalizationService _localizationService;

    public TranslatorBookBusinessRules(ITranslatorBookRepository translatorBookRepository, ILocalizationService localizationService)
    {
        _translatorBookRepository = translatorBookRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, TranslatorBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task TranslatorBookShouldExistWhenSelected(TranslatorBook? translatorBook)
    {
        if (translatorBook == null)
            await throwBusinessException(TranslatorBooksBusinessMessages.TranslatorBookNotExists);
    }

    public async Task TranslatorBookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        TranslatorBook? translatorBook = await _translatorBookRepository.GetAsync(
            predicate: tb => tb.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TranslatorBookShouldExistWhenSelected(translatorBook);
    }
}