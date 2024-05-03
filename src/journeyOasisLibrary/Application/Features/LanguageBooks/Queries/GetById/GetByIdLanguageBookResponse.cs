using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.LanguageBooks.Queries.GetById;

public class GetByIdLanguageBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid LanguageId { get; set; }
    public Book? Book { get; set; }
    public Language? Language { get; set; }
}