using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.WriterBooks.Queries.GetList;

public class GetListWriterBookListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid WriterId { get; set; }
    public Book? Book { get; set; }
    public Writer? Writer { get; set; }
}