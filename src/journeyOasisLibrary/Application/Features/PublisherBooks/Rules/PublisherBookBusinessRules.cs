using Application.Features.PublisherBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.PublisherBooks.Rules;

public class PublisherBookBusinessRules : BaseBusinessRules
{
    private readonly IPublisherBookRepository _publisherBookRepository;
    private readonly ILocalizationService _localizationService;

    public PublisherBookBusinessRules(IPublisherBookRepository publisherBookRepository, ILocalizationService localizationService)
    {
        _publisherBookRepository = publisherBookRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, PublisherBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task PublisherBookShouldExistWhenSelected(PublisherBook? publisherBook)
    {
        if (publisherBook == null)
            await throwBusinessException(PublisherBooksBusinessMessages.PublisherBookNotExists);
    }

    public async Task PublisherBookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        PublisherBook? publisherBook = await _publisherBookRepository.GetAsync(
            predicate: pb => pb.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PublisherBookShouldExistWhenSelected(publisherBook);
    }
}