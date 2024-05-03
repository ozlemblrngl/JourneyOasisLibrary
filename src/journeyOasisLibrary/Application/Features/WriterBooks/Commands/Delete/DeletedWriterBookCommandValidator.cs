using FluentValidation;

namespace Application.Features.WriterBooks.Commands.Delete;

public class DeleteWriterBookCommandValidator : AbstractValidator<DeleteWriterBookCommand>
{
    public DeleteWriterBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}