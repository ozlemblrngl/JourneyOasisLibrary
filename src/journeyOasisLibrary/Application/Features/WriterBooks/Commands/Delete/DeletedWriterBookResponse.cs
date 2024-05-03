using NArchitecture.Core.Application.Responses;

namespace Application.Features.WriterBooks.Commands.Delete;

public class DeletedWriterBookResponse : IResponse
{
    public Guid Id { get; set; }
}