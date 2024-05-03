using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.PublisherBooks.Commands.Create;

public class CreatedPublisherBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid PublisherId { get; set; }
    public Book? Book { get; set; }
    public Publisher? Publisher { get; set; }
}