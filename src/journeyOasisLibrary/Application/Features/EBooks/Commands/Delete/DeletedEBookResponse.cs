using NArchitecture.Core.Application.Responses;

namespace Application.Features.EBooks.Commands.Delete;

public class DeletedEBookResponse : IResponse
{
    public Guid Id { get; set; }
}