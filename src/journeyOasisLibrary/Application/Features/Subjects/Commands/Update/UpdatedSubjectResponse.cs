using NArchitecture.Core.Application.Responses;

namespace Application.Features.Subjects.Commands.Update;

public class UpdatedSubjectResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}