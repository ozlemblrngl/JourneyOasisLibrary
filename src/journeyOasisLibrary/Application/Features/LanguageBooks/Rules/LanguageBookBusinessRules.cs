using Application.Features.LanguageBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.LanguageBooks.Rules;

public class LanguageBookBusinessRules : BaseBusinessRules
{
    private readonly ILanguageBookRepository _languageBookRepository;
    private readonly ILocalizationService _localizationService;

    public LanguageBookBusinessRules(ILanguageBookRepository languageBookRepository, ILocalizationService localizationService)
    {
        _languageBookRepository = languageBookRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, LanguageBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task LanguageBookShouldExistWhenSelected(LanguageBook? languageBook)
    {
        if (languageBook == null)
            await throwBusinessException(LanguageBooksBusinessMessages.LanguageBookNotExists);
    }

    public async Task LanguageBookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LanguageBook? languageBook = await _languageBookRepository.GetAsync(
            predicate: lb => lb.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LanguageBookShouldExistWhenSelected(languageBook);
    }
}