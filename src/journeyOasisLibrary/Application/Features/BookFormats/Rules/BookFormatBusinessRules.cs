using Application.Features.BookFormats.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.BookFormats.Rules;

public class BookFormatBusinessRules : BaseBusinessRules
{
    private readonly IBookFormatRepository _bookFormatRepository;
    private readonly ILocalizationService _localizationService;

    public BookFormatBusinessRules(IBookFormatRepository bookFormatRepository, ILocalizationService localizationService)
    {
        _bookFormatRepository = bookFormatRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BookFormatsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BookFormatShouldExistWhenSelected(BookFormat? bookFormat)
    {
        if (bookFormat == null)
            await throwBusinessException(BookFormatsBusinessMessages.BookFormatNotExists);
    }

    public async Task BookFormatIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        BookFormat? bookFormat = await _bookFormatRepository.GetAsync(
            predicate: bf => bf.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BookFormatShouldExistWhenSelected(bookFormat);
    }
}