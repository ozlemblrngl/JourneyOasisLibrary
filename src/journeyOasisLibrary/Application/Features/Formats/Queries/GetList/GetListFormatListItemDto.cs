using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Formats.Queries.GetList;

public class GetListFormatListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}