using Application.Features.Formats.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Formats.Rules;

public class FormatBusinessRules : BaseBusinessRules
{
    private readonly IFormatRepository _formatRepository;
    private readonly ILocalizationService _localizationService;

    public FormatBusinessRules(IFormatRepository formatRepository, ILocalizationService localizationService)
    {
        _formatRepository = formatRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, FormatsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task FormatShouldExistWhenSelected(Format? format)
    {
        if (format == null)
            await throwBusinessException(FormatsBusinessMessages.FormatNotExists);
    }

    public async Task FormatIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Format? format = await _formatRepository.GetAsync(
            predicate: f => f.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FormatShouldExistWhenSelected(format);
    }
}