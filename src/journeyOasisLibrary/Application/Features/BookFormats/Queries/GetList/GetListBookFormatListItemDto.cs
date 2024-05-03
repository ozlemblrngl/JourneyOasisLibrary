using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.BookFormats.Queries.GetList;

public class GetListBookFormatListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid FormatId { get; set; }
    public Book? Book { get; set; }
    public Format? Format { get; set; }
}