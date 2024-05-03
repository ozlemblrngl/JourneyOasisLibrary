using NArchitecture.Core.Application.Responses;

namespace Application.Features.Writers.Commands.Create;

public class CreatedWriterResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}