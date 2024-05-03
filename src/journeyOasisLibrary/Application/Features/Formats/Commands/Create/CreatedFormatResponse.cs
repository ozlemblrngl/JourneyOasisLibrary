using NArchitecture.Core.Application.Responses;

namespace Application.Features.Formats.Commands.Create;

public class CreatedFormatResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}