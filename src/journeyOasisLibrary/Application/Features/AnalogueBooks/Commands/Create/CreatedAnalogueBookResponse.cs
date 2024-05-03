using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.AnalogueBooks.Commands.Create;

public class CreatedAnalogueBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }
}