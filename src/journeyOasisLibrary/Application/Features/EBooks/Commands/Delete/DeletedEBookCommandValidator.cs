using FluentValidation;

namespace Application.Features.EBooks.Commands.Delete;

public class DeleteEBookCommandValidator : AbstractValidator<DeleteEBookCommand>
{
    public DeleteEBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}