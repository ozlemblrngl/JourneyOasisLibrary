using FluentValidation;

namespace Application.Features.AnalogueBooks.Commands.Update;

public class UpdateAnalogueBookCommandValidator : AbstractValidator<UpdateAnalogueBookCommand>
{
    public UpdateAnalogueBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookFormatId).NotEmpty();
        RuleFor(c => c.BookFormat).NotEmpty();
    }
}