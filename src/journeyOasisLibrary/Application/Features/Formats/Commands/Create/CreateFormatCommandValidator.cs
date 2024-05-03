using FluentValidation;

namespace Application.Features.Formats.Commands.Create;

public class CreateFormatCommandValidator : AbstractValidator<CreateFormatCommand>
{
    public CreateFormatCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}