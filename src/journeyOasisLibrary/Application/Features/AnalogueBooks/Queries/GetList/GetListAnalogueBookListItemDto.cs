using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.AnalogueBooks.Queries.GetList;

public class GetListAnalogueBookListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }
}