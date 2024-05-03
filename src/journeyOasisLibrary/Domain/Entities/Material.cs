using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Material : Entity<Guid>
{
    public string Name { get; set; }
}
