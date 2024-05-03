using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.AnalogueBooks.Commands.Update;

public class UpdatedAnalogueBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }
}