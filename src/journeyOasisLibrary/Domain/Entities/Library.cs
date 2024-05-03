using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Library : Entity<Guid>
{
    public string Name { get; set; }

    public ICollection<Material> Materials { get; set; }
}
