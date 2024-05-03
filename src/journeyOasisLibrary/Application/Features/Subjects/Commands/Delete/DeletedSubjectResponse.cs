using NArchitecture.Core.Application.Responses;

namespace Application.Features.Subjects.Commands.Delete;

public class DeletedSubjectResponse : IResponse
{
    public Guid Id { get; set; }
}