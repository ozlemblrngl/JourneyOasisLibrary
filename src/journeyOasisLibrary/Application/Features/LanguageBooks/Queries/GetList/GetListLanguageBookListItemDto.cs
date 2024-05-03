using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.LanguageBooks.Queries.GetList;

public class GetListLanguageBookListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid LanguageId { get; set; }
    public Book? Book { get; set; }
    public Language? Language { get; set; }
}