using FluentValidation;

namespace Application.Features.TranslatorBooks.Commands.Update;

public class UpdateTranslatorBookCommandValidator : AbstractValidator<UpdateTranslatorBookCommand>
{
    public UpdateTranslatorBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.TranslatorId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Translator).NotEmpty();
    }
}