using FluentValidation;

namespace Application.Features.LanguageBooks.Commands.Create;

public class CreateLanguageBookCommandValidator : AbstractValidator<CreateLanguageBookCommand>
{
    public CreateLanguageBookCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.LanguageId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Language).NotEmpty();
    }
}