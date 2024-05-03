using FluentValidation;

namespace Application.Features.TranslatorBooks.Commands.Create;

public class CreateTranslatorBookCommandValidator : AbstractValidator<CreateTranslatorBookCommand>
{
    public CreateTranslatorBookCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.TranslatorId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Translator).NotEmpty();
    }
}