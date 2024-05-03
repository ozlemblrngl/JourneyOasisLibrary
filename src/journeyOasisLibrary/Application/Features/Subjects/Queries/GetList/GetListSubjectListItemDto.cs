using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Subjects.Queries.GetList;

public class GetListSubjectListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}