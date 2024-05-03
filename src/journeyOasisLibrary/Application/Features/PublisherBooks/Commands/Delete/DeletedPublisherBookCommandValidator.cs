using FluentValidation;

namespace Application.Features.PublisherBooks.Commands.Delete;

public class DeletePublisherBookCommandValidator : AbstractValidator<DeletePublisherBookCommand>
{
    public DeletePublisherBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}