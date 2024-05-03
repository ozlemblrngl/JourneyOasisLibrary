using NArchitecture.Core.Application.Responses;

namespace Application.Features.Formats.Queries.GetById;

public class GetByIdFormatResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}