using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.EBooks.Commands.Create;

public class CreatedEBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }
}