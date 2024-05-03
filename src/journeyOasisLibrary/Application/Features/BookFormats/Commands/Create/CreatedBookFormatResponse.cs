using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.BookFormats.Commands.Create;

public class CreatedBookFormatResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid FormatId { get; set; }
    public Book? Book { get; set; }
    public Format? Format { get; set; }
}