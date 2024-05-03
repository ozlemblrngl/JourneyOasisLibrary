using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.PublisherBooks.Queries.GetList;

public class GetListPublisherBookListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid PublisherId { get; set; }
    public Book? Book { get; set; }
    public Publisher? Publisher { get; set; }
}