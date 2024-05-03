using Application.Features.Writers.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Writers.Rules;

public class WriterBusinessRules : BaseBusinessRules
{
    private readonly IWriterRepository _writerRepository;
    private readonly ILocalizationService _localizationService;

    public WriterBusinessRules(IWriterRepository writerRepository, ILocalizationService localizationService)
    {
        _writerRepository = writerRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, WritersBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task WriterShouldExistWhenSelected(Writer? writer)
    {
        if (writer == null)
            await throwBusinessException(WritersBusinessMessages.WriterNotExists);
    }

    public async Task WriterIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Writer? writer = await _writerRepository.GetAsync(
            predicate: w => w.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await WriterShouldExistWhenSelected(writer);
    }
}