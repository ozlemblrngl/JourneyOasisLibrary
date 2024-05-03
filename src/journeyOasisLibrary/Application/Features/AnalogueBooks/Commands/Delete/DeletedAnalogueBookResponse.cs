using NArchitecture.Core.Application.Responses;

namespace Application.Features.AnalogueBooks.Commands.Delete;

public class DeletedAnalogueBookResponse : IResponse
{
    public Guid Id { get; set; }
}