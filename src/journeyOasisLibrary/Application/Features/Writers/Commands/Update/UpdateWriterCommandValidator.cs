using FluentValidation;

namespace Application.Features.Writers.Commands.Update;

public class UpdateWriterCommandValidator : AbstractValidator<UpdateWriterCommand>
{
    public UpdateWriterCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}