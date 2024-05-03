using FluentValidation;

namespace Application.Features.PublisherBooks.Commands.Update;

public class UpdatePublisherBookCommandValidator : AbstractValidator<UpdatePublisherBookCommand>
{
    public UpdatePublisherBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.PublisherId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Publisher).NotEmpty();
    }
}