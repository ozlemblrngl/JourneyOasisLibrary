using FluentValidation;

namespace Application.Features.EBooks.Commands.Create;

public class CreateEBookCommandValidator : AbstractValidator<CreateEBookCommand>
{
    public CreateEBookCommandValidator()
    {
        RuleFor(c => c.BookFormatId).NotEmpty();
        RuleFor(c => c.BookFormat).NotEmpty();
    }
}