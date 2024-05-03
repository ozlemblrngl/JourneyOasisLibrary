using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.EBooks.Queries.GetById;

public class GetByIdEBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookFormatId { get; set; }
    public BookFormat? BookFormat { get; set; }
}