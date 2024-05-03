using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Writer : Entity<Guid>
{

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<WriterBook> WriterBooks { get; set; }
}
