using FluentValidation;

namespace Application.Features.Subjects.Commands.Create;

public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}