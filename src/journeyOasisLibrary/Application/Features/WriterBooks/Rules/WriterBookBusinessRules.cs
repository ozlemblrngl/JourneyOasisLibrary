using Application.Features.WriterBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.WriterBooks.Rules;

public class WriterBookBusinessRules : BaseBusinessRules
{
    private readonly IWriterBookRepository _writerBookRepository;
    private readonly ILocalizationService _localizationService;

    public WriterBookBusinessRules(IWriterBookRepository writerBookRepository, ILocalizationService localizationService)
    {
        _writerBookRepository = writerBookRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, WriterBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task WriterBookShouldExistWhenSelected(WriterBook? writerBook)
    {
        if (writerBook == null)
            await throwBusinessException(WriterBooksBusinessMessages.WriterBookNotExists);
    }

    public async Task WriterBookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        WriterBook? writerBook = await _writerBookRepository.GetAsync(
            predicate: wb => wb.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await WriterBookShouldExistWhenSelected(writerBook);
    }
}