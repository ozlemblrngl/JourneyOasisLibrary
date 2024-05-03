using NArchitecture.Core.Application.Responses;

namespace Application.Features.BookFormats.Commands.Delete;

public class DeletedBookFormatResponse : IResponse
{
    public Guid Id { get; set; }
}