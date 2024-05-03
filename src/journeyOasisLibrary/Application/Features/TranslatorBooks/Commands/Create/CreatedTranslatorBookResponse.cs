using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.TranslatorBooks.Commands.Create;

public class CreatedTranslatorBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid TranslatorId { get; set; }
    public Book? Book { get; set; }
    public Translator? Translator { get; set; }
}