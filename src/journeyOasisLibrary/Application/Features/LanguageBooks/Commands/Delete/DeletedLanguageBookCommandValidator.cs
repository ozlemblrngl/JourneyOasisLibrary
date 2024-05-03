using FluentValidation;

namespace Application.Features.LanguageBooks.Commands.Delete;

public class DeleteLanguageBookCommandValidator : AbstractValidator<DeleteLanguageBookCommand>
{
    public DeleteLanguageBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}