using NArchitecture.Core.Application.Responses;

namespace Application.Features.PublisherBooks.Commands.Delete;

public class DeletedPublisherBookResponse : IResponse
{
    public Guid Id { get; set; }
}