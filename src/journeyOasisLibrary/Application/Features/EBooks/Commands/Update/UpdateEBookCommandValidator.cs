using FluentValidation;

namespace Application.Features.EBooks.Commands.Update;

public class UpdateEBookCommandValidator : AbstractValidator<UpdateEBookCommand>
{
    public UpdateEBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookFormatId).NotEmpty();
        RuleFor(c => c.BookFormat).NotEmpty();
    }
}