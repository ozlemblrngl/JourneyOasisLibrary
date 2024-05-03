using FluentValidation;

namespace Application.Features.AnalogueBooks.Commands.Delete;

public class DeleteAnalogueBookCommandValidator : AbstractValidator<DeleteAnalogueBookCommand>
{
    public DeleteAnalogueBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}