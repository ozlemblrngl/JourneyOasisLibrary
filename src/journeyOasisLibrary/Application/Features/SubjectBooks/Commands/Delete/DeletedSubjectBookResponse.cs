using NArchitecture.Core.Application.Responses;

namespace Application.Features.SubjectBooks.Commands.Delete;

public class DeletedSubjectBookResponse : IResponse
{
    public Guid Id { get; set; }
}