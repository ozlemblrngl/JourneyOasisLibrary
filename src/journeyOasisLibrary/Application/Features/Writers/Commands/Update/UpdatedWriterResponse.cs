using NArchitecture.Core.Application.Responses;

namespace Application.Features.Writers.Commands.Update;

public class UpdatedWriterResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}