using NArchitecture.Core.Application.Responses;

namespace Application.Features.Writers.Commands.Delete;

public class DeletedWriterResponse : IResponse
{
    public Guid Id { get; set; }
}