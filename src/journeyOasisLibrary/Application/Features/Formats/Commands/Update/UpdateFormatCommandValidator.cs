using FluentValidation;

namespace Application.Features.Formats.Commands.Update;

public class UpdateFormatCommandValidator : AbstractValidator<UpdateFormatCommand>
{
    public UpdateFormatCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}