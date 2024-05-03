using FluentValidation;

namespace Application.Features.TranslatorBooks.Commands.Delete;

public class DeleteTranslatorBookCommandValidator : AbstractValidator<DeleteTranslatorBookCommand>
{
    public DeleteTranslatorBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}