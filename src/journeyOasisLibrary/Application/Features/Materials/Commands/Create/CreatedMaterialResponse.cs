using NArchitecture.Core.Application.Responses;

namespace Application.Features.Materials.Commands.Create;

public class CreatedMaterialResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}