using NArchitecture.Core.Application.Responses;

namespace Application.Features.Subjects.Queries.GetById;

public class GetByIdSubjectResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}