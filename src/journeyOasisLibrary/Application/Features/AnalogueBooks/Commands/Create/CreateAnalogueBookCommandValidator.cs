using FluentValidation;

namespace Application.Features.AnalogueBooks.Commands.Create;

public class CreateAnalogueBookCommandValidator : AbstractValidator<CreateAnalogueBookCommand>
{
    public CreateAnalogueBookCommandValidator()
    {
        RuleFor(c => c.BookFormatId).NotEmpty();
        RuleFor(c => c.BookFormat).NotEmpty();
    }
}