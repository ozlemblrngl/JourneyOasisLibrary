using NArchitecture.Core.Application.Responses;

namespace Application.Features.Materials.Commands.Update;

public class UpdatedMaterialResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}