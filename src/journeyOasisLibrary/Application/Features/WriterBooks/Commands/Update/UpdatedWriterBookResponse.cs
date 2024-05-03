using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.WriterBooks.Commands.Update;

public class UpdatedWriterBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid WriterId { get; set; }
    public Book? Book { get; set; }
    public Writer? Writer { get; set; }
}