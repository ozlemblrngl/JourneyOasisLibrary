using NArchitecture.Core.Application.Responses;

namespace Application.Features.Writers.Queries.GetById;

public class GetByIdWriterResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}