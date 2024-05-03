using Application.Features.SubjectBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.SubjectBooks.Rules;

public class SubjectBookBusinessRules : BaseBusinessRules
{
    private readonly ISubjectBookRepository _subjectBookRepository;
    private readonly ILocalizationService _localizationService;

    public SubjectBookBusinessRules(ISubjectBookRepository subjectBookRepository, ILocalizationService localizationService)
    {
        _subjectBookRepository = subjectBookRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, SubjectBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task SubjectBookShouldExistWhenSelected(SubjectBook? subjectBook)
    {
        if (subjectBook == null)
            await throwBusinessException(SubjectBooksBusinessMessages.SubjectBookNotExists);
    }

    public async Task SubjectBookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        SubjectBook? subjectBook = await _subjectBookRepository.GetAsync(
            predicate: sb => sb.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SubjectBookShouldExistWhenSelected(subjectBook);
    }
}