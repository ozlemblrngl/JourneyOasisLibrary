using FluentValidation;

namespace Application.Features.Formats.Commands.Delete;

public class DeleteFormatCommandValidator : AbstractValidator<DeleteFormatCommand>
{
    public DeleteFormatCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}