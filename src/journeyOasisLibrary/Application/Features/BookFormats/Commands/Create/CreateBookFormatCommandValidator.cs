using FluentValidation;

namespace Application.Features.BookFormats.Commands.Create;

public class CreateBookFormatCommandValidator : AbstractValidator<CreateBookFormatCommand>
{
    public CreateBookFormatCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.FormatId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Format).NotEmpty();
    }
}