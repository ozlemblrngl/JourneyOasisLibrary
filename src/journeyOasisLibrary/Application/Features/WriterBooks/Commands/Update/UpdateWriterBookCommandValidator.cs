using FluentValidation;

namespace Application.Features.WriterBooks.Commands.Update;

public class UpdateWriterBookCommandValidator : AbstractValidator<UpdateWriterBookCommand>
{
    public UpdateWriterBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.WriterId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Writer).NotEmpty();
    }
}