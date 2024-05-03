using NArchitecture.Core.Application.Responses;

namespace Application.Features.Subjects.Commands.Create;

public class CreatedSubjectResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}