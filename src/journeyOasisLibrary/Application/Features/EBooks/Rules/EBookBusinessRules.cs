using Application.Features.EBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.EBooks.Rules;

public class EBookBusinessRules : BaseBusinessRules
{
    private readonly IEBookRepository _eBookRepository;
    private readonly ILocalizationService _localizationService;

    public EBookBusinessRules(IEBookRepository eBookRepository, ILocalizationService localizationService)
    {
        _eBookRepository = eBookRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, EBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task EBookShouldExistWhenSelected(EBook? eBook)
    {
        if (eBook == null)
            await throwBusinessException(EBooksBusinessMessages.EBookNotExists);
    }

    public async Task EBookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        EBook? eBook = await _eBookRepository.GetAsync(
            predicate: eb => eb.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await EBookShouldExistWhenSelected(eBook);
    }
}