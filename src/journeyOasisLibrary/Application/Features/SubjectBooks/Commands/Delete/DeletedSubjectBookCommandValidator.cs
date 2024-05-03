using FluentValidation;

namespace Application.Features.SubjectBooks.Commands.Delete;

public class DeleteSubjectBookCommandValidator : AbstractValidator<DeleteSubjectBookCommand>
{
    public DeleteSubjectBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}