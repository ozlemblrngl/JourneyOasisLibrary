using NArchitecture.Core.Application.Responses;

namespace Application.Features.Formats.Commands.Delete;

public class DeletedFormatResponse : IResponse
{
    public Guid Id { get; set; }
}