using Application.Features.Subjects.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Subjects.Rules;

public class SubjectBusinessRules : BaseBusinessRules
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ILocalizationService _localizationService;

    public SubjectBusinessRules(ISubjectRepository subjectRepository, ILocalizationService localizationService)
    {
        _subjectRepository = subjectRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, SubjectsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task SubjectShouldExistWhenSelected(Subject? subject)
    {
        if (subject == null)
            await throwBusinessException(SubjectsBusinessMessages.SubjectNotExists);
    }

    public async Task SubjectIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Subject? subject = await _subjectRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SubjectShouldExistWhenSelected(subject);
    }
}