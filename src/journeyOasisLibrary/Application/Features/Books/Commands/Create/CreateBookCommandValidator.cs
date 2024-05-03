using FluentValidation;

namespace Application.Features.Books.Commands.Create;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(c => c.MaterialId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.Isbn).NotEmpty();
        RuleFor(c => c.Edition).NotEmpty();
        RuleFor(c => c.ReleaseDate).NotEmpty();
        RuleFor(c => c.PhysicalInfo).NotEmpty();
        RuleFor(c => c.Note).NotEmpty();
        RuleFor(c => c.Material).NotEmpty();
    }
}