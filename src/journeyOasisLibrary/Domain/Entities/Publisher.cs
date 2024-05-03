using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Publisher : Entity<Guid>
{
    public string Name { get; set; }

    public ICollection<PublisherBook> PublisherBooks { get; set; }
}
