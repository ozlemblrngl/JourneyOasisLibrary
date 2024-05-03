using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.AnalogueBooks.Queries.GetById;

public class GetByIdAnalogueBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }
}