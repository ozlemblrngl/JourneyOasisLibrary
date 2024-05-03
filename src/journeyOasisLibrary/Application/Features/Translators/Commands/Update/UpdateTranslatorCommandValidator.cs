using FluentValidation;

namespace Application.Features.Translators.Commands.Update;

public class UpdateTranslatorCommandValidator : AbstractValidator<UpdateTranslatorCommand>
{
    public UpdateTranslatorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}