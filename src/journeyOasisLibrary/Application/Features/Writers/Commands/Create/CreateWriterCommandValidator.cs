using FluentValidation;

namespace Application.Features.Writers.Commands.Create;

public class CreateWriterCommandValidator : AbstractValidator<CreateWriterCommand>
{
    public CreateWriterCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}