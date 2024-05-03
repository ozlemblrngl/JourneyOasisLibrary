using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.SubjectBooks.Commands.Update;

public class UpdatedSubjectBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid SubjectId { get; set; }
    public Book? Book { get; set; }
    public Subject? Subject { get; set; }
}