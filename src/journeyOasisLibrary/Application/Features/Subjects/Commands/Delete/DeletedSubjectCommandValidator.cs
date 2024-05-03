using FluentValidation;

namespace Application.Features.Subjects.Commands.Delete;

public class DeleteSubjectCommandValidator : AbstractValidator<DeleteSubjectCommand>
{
    public DeleteSubjectCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}