using Application.Features.AnalogueBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.AnalogueBooks.Rules;

public class AnalogueBookBusinessRules : BaseBusinessRules
{
    private readonly IAnalogueBookRepository _analogueBookRepository;
    private readonly ILocalizationService _localizationService;

    public AnalogueBookBusinessRules(IAnalogueBookRepository analogueBookRepository, ILocalizationService localizationService)
    {
        _analogueBookRepository = analogueBookRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AnalogueBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AnalogueBookShouldExistWhenSelected(AnalogueBook? analogueBook)
    {
        if (analogueBook == null)
            await throwBusinessException(AnalogueBooksBusinessMessages.AnalogueBookNotExists);
    }

    public async Task AnalogueBookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        AnalogueBook? analogueBook = await _analogueBookRepository.GetAsync(
            predicate: ab => ab.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AnalogueBookShouldExistWhenSelected(analogueBook);
    }
}