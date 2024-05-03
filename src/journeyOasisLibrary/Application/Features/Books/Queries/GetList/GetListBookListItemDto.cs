using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Books.Queries.GetList;

public class GetListBookListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid MaterialId { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Isbn { get; set; }
    public string Edition { get; set; }
    public string ReleaseDate { get; set; }
    public string PhysicalInfo { get; set; }
    public string Note { get; set; }
    public Material? Material { get; set; }
}