using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.SubjectBooks.Queries.GetList;

public class GetListSubjectBookListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid SubjectId { get; set; }
    public Book? Book { get; set; }
    public Subject? Subject { get; set; }
}