using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.LanguageBooks.Commands.Create;

public class CreatedLanguageBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid LanguageId { get; set; }
    public Book? Book { get; set; }
    public Language? Language { get; set; }
}