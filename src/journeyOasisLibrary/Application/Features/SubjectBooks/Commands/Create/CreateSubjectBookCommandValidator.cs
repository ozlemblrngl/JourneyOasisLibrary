using FluentValidation;

namespace Application.Features.SubjectBooks.Commands.Create;

public class CreateSubjectBookCommandValidator : AbstractValidator<CreateSubjectBookCommand>
{
    public CreateSubjectBookCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.SubjectId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Subject).NotEmpty();
    }
}