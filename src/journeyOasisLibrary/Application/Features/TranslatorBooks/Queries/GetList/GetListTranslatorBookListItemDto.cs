using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.TranslatorBooks.Queries.GetList;

public class GetListTranslatorBookListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid TranslatorId { get; set; }
    public Book? Book { get; set; }
    public Translator? Translator { get; set; }
}