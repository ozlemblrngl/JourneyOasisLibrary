using FluentValidation;

namespace Application.Features.BookFormats.Commands.Update;

public class UpdateBookFormatCommandValidator : AbstractValidator<UpdateBookFormatCommand>
{
    public UpdateBookFormatCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.FormatId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Format).NotEmpty();
    }
}