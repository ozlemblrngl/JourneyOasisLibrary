using FluentValidation;

namespace Application.Features.WriterBooks.Commands.Create;

public class CreateWriterBookCommandValidator : AbstractValidator<CreateWriterBookCommand>
{
    public CreateWriterBookCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.WriterId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Writer).NotEmpty();
    }
}