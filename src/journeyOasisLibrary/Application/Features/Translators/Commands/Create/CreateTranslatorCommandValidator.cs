using FluentValidation;

namespace Application.Features.Translators.Commands.Create;

public class CreateTranslatorCommandValidator : AbstractValidator<CreateTranslatorCommand>
{
    public CreateTranslatorCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}