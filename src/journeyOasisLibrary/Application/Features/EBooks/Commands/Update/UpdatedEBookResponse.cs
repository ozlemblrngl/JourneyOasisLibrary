using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.EBooks.Commands.Update;

public class UpdatedEBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }
}