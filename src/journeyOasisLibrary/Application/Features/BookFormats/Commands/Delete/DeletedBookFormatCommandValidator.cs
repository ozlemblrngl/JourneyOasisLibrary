using FluentValidation;

namespace Application.Features.BookFormats.Commands.Delete;

public class DeleteBookFormatCommandValidator : AbstractValidator<DeleteBookFormatCommand>
{
    public DeleteBookFormatCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}