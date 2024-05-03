using FluentValidation;

namespace Application.Features.Writers.Commands.Delete;

public class DeleteWriterCommandValidator : AbstractValidator<DeleteWriterCommand>
{
    public DeleteWriterCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}