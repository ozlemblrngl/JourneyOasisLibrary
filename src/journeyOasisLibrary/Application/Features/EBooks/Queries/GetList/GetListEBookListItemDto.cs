using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.EBooks.Queries.GetList;

public class GetListEBookListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }
}