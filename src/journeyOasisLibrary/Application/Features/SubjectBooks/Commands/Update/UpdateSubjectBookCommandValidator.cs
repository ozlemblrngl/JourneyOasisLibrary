using FluentValidation;

namespace Application.Features.SubjectBooks.Commands.Update;

public class UpdateSubjectBookCommandValidator : AbstractValidator<UpdateSubjectBookCommand>
{
    public UpdateSubjectBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.SubjectId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Subject).NotEmpty();
    }
}