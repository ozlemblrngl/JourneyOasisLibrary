using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.BookFormats.Queries.GetById;

public class GetByIdBookFormatResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid FormatId { get; set; }
    public Book? Book { get; set; }
    public Format? Format { get; set; }
}