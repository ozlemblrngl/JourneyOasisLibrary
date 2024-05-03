using NArchitecture.Core.Application.Responses;

namespace Application.Features.Translators.Commands.Create;

public class CreatedTranslatorResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}