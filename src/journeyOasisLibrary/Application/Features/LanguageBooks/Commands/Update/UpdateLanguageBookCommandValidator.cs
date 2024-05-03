using FluentValidation;

namespace Application.Features.LanguageBooks.Commands.Update;

public class UpdateLanguageBookCommandValidator : AbstractValidator<UpdateLanguageBookCommand>
{
    public UpdateLanguageBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.LanguageId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Language).NotEmpty();
    }
}