using NArchitecture.Core.Application.Responses;

namespace Application.Features.Translators.Commands.Update;

public class UpdatedTranslatorResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}