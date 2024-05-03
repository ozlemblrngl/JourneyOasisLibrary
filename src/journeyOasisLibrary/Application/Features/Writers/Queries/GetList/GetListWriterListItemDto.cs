using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Writers.Queries.GetList;

public class GetListWriterListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}