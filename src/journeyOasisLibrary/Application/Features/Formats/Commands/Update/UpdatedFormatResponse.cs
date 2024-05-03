using NArchitecture.Core.Application.Responses;

namespace Application.Features.Formats.Commands.Update;

public class UpdatedFormatResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}