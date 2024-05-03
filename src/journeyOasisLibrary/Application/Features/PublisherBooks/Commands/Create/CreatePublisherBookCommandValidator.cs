using FluentValidation;

namespace Application.Features.PublisherBooks.Commands.Create;

public class CreatePublisherBookCommandValidator : AbstractValidator<CreatePublisherBookCommand>
{
    public CreatePublisherBookCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.PublisherId).NotEmpty();
        RuleFor(c => c.Book).NotEmpty();
        RuleFor(c => c.Publisher).NotEmpty();
    }
}